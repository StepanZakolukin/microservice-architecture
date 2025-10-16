using UserService.Domain.Models;

namespace UserService.Application.Interfaces;

internal interface IRefreshTokenGenerator
{
    Task<RefreshToken> GenerateAsync(Guid userId, CancellationToken cancellationToken);
}