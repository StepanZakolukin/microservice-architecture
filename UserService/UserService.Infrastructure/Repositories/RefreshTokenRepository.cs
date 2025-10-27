using Microsoft.EntityFrameworkCore;
using UserService.Application.InterfaceRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ServiceDbContext _dbContext;

    public RefreshTokenRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public async Task<RefreshToken?> GetAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(token => token.Token == refreshToken, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}