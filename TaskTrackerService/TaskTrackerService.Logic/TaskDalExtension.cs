using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Board.Models.Response;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Logic;

internal static class TaskDalExtension
{
    public static TaskDal Create(CreateTaskLogic dto, PriorityDal? priority)
    {
        return new TaskDal
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatorId = dto.CreatorId,
            Deadline = dto.Deadline,
            Priority = priority
        };
    }

    public static TaskResponse ConvertToTaskResult(this TaskDal task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Completed = task.Completed,
            Title = task.Title,
            Description = task.Description,
            Deadline = task.Deadline,
            Priority = task.Priority is not null
                ? new PriorityResponse { Id = task.PriorityId, Name = task.Priority.Name }
                : null
        };
    }
}