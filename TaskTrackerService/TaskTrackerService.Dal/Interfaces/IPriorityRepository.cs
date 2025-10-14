using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface IPriorityRepository
{
    Task<PriorityDal?> GetPriorityAsync(Guid priorityId, CancellationToken cancellationToken);
}