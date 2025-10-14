using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface ITaskRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Update(TaskDal task);
    void Delete(TaskDal task);
    Task<TaskDal?> GetTaskAsync(Guid taskId, CancellationToken cancellationToken);
}