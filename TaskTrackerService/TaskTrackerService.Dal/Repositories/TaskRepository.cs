using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

internal class TaskRepository : ITaskRepository
{
    private readonly ServiceDbContext _dbContext;

    public TaskRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(TaskDal task)
    {
        _dbContext.Tasks.Update(task);
    }

    public void Delete(TaskDal task)
    {
        _dbContext.Tasks.Remove(task);
    }

    public async Task<TaskDal?> GetTaskAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Include(task => task.Priority)
            .Where(task => task.Id == taskId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}