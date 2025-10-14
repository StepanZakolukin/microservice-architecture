using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Logic.Task;

public static class TaskDalExtension
{
    public static TaskDal Create(CreateTaskLogic dto, PriorityDal? priority)
    {
        return new TaskDal
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatorId = dto.CreatorId,
            Deadline = dto.Deadline,
            ColumnId = dto.ColumnId,
            Priority = priority,
            PriorityId = dto.PriorityId,
        };
    }
}