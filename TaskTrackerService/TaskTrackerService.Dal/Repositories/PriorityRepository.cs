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
}