using TaskTrackerService.Api.Controllers.Task.Cammon;

namespace TaskTrackerService.Api.Controllers.Task.Request;

public record UpdateTaskRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool Completed { get; init; }
    public required DateTime? Deadline { get; init; }
    public Guid PriorityId { get; init; }
}