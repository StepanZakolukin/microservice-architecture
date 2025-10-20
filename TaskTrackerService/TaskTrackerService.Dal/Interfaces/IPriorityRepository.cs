using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface IPriorityRepository
{
    Task<PriorityDal?> GetPriorityAsync(Guid priorityId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Update(PriorityDal priority);
    void Delete(PriorityDal priority);
    Task AddAsync(PriorityDal priority, CancellationToken cancellationToken);
    Task<ICollection<PriorityDal>> GetAllAsync(CancellationToken cancellationToken);
}