using FluentResults;
using UserService.Application.Auth.Command;
using UserService.Application.Auth.Dto;

namespace UserService.Application.Auth.Interfaces;

public interface IAuthManager
{
    Task<Result<RegisterInfoResponse>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);

    Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken);

    Task<Result<LoginInfoResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken);

    Task<Result<string>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}