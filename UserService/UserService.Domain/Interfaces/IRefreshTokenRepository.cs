using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        
    Task<RefreshToken?> GetAsync(string refreshToken, CancellationToken cancellationToken);
        
    Task SaveChangesAsync(CancellationToken cancellationToken);
}