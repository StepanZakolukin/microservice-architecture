using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

internal class PriorityRepository : IPriorityRepository
{
    private readonly ServiceDbContext _dbContext;

    public PriorityRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PriorityDal?> GetPriorityAsync(Guid priorityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Priorities.FindAsync([priorityId, cancellationToken], cancellationToken: cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(PriorityDal priority)
    { 
        _dbContext.Priorities.Update(priority);
    }

    public void Delete(PriorityDal priority)
    {
        _dbContext.Priorities.Remove(priority);
    }

    public async Task AddAsync(PriorityDal priority, CancellationToken cancellationToken)
    {
        await _dbContext.Priorities.AddAsync(priority, cancellationToken);
    }

    public async Task<ICollection<PriorityDal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Priorities.ToListAsync(cancellationToken);
    }
}