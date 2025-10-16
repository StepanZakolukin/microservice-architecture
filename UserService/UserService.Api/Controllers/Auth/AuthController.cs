using System.Security.Claims;
using Core.Errors;
using UserService.Application.Command;
using UserService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Controllers.User.Request;
using UserService.Application.Dto;

namespace UserService.Api.Controllers.User;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Регистрация пользователя с получением кода подтверждения на email
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RegisterInfoResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterInfoRequest dto, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand
        {
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
        
        var result = await _authService.RegisterAsync(command, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Аутентификация пользователя и получение JWT-токена
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType<LoginInfoResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginInfoRequest dto, CancellationToken cancellationToken)
    {
        var loginResult = await _authService.LoginAsync(dto.Email, dto.Password, cancellationToken);
        return loginResult.ToActionResult();
    }

    /// <summary>
    /// Обновление истекшего токена по refresh-токену
    /// </summary>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<RegisterInfoResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshAsync([FromBody] string refreshToken,  CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Отзывает (инвалидирует) refresh-токен, завершает сессию пользователя
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAsync([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var result = await _authService.LogoutAsync(refreshToken, cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Получить информацию о текущем пользователе по токену
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public IActionResult GetMe()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new { id, email, role });
    }
}