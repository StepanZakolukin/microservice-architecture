using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ServiceDbContext _dbContext;

    public UserRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
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
}