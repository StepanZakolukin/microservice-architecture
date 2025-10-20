using Microsoft.EntityFrameworkCore;
using UserService.Application.InterfaceRepositories;

namespace UserService.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ServiceDbContext _dbContext;

    public UserRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.User?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Where(user => user.Id == userId)
            .Include(user => user.Notifications)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.User>> GetAllUsers(CancellationToken cancellationToken)
    {
        return await _dbContext.Users.ToListAsync(cancellationToken);
    }
}