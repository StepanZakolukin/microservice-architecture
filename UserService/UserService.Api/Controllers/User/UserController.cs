using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Controllers.User.Response;
using UserService.Api.Services.Interfaces;
using UserService.Application.User;
using UserService.Application.User.Dto;

namespace UserService.Api.Controllers.User;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserManager _userManager;
    
    public UserController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    [HttpPatch("{user-id}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute(Name ="user-id")] Guid userId,
        [FromBody] UpdateUserDto dto,
        [FromServices] IUserContext userContext)
    {
        if (userContext.UserId != userId)
            return Forbid();

        var result = await _userManager.UpdateUserAsync(userId, dto);

        return result.ToActionResult();
    }

    /// <summary>
    /// Найти пользователя
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ICollection<ShortenedUserResponce>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> FindUserAsync(
        [FromQuery] UserPageQueryFilter filter,
        CancellationToken cancellationToken)
    {
        return Ok((await _userManager.FindUsersAsync(filter, cancellationToken)).Convert(Convert));
    }

    private static ShortenedUserResponce Convert(Domain.Entities.User user)
    {
        return new ShortenedUserResponce
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!
        };
    }
    
    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    [HttpGet("{user-id:guid}")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserAsync(
        [FromRoute(Name = "user-id")] Guid userId,
        [FromServices] IUserContext userContext)
    {
        if (userId != userContext.UserId)
            return Forbid();
        
        var result = await _userManager.GetUserAsync(userId);
        
        return result
            .Map(user => new UserResponse 
            {
                Email = user.Email!,
                LastName = user.LastName,
                FirstName = user.FirstName,
                CreateAt = user.CreateAt
            }).ToActionResult();
    }
}