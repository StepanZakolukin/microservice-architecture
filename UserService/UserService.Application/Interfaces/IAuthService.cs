using FluentResults;
using UserService.Application.Command;
using UserService.Application.Dto;

namespace UserService.Application.Interfaces;

public interface IAuthService
{
    Task<Result<RegisterInfoResponse>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);

    Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken);

    Task<Result<LoginInfoResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken);

    Task<Result<string>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}