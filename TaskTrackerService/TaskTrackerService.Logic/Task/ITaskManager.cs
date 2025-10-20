using FluentResults;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Logic.Task;

public interface ITaskManager
{
    Task<Result<Guid>> CreateTaskAsync(CreateTaskLogic dto, CancellationToken cancellationToken);
    Task<Result> UpdateTaskAsync(UpdateTaskLogic dto, CancellationToken cancellationToken);
    Task<Result> MoveTaskAsync(MoveTaskLogic dto, CancellationToken cancellationToken);
    Task<Result> DeleteTaskAsync(DeleteTaskLogic dto, CancellationToken cancellationToken);
}