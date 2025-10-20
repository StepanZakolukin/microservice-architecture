namespace TaskTrackerService.Logic.Task.Models;

public record DeleteTaskLogic
{
    public required Guid TaskId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}