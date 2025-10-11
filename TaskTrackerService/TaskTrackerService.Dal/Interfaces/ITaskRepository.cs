using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskDal task, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Update(TaskDal task);
    void Delete(TaskDal task);
    void GetById(Guid id, CancellationToken cancellationToken);
}