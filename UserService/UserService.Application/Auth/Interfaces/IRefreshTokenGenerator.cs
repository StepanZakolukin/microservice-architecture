using UserService.Domain.Entities;

namespace UserService.Application.Auth.Interfaces;

internal interface IRefreshTokenGenerator
{
    Task<RefreshToken> GenerateAsync(Guid userId, CancellationToken cancellationToken);
}