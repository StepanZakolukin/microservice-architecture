namespace TaskTrackerService.Logic.Task.Models;

public record MoveTaskLogic
{
    public required Guid TaskId { get; init; }
    public required int NewNumber { get; init; }
    public required Guid NewColumnId {get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}