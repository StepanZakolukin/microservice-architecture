using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Services.Interfaces;
using UserService.Application.User;
using UserService.Application.User.Dto;

namespace UserService.Api.Controllers.User;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;
    
    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [Authorize]
    [HttpPatch("{user-id}")]
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
}