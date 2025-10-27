using UserService.Domain.Entities;

namespace UserService.Application.InterfaceRepositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        
    Task<RefreshToken?> GetAsync(string refreshToken, CancellationToken cancellationToken);
        
    Task SaveChangesAsync(CancellationToken cancellationToken);
}