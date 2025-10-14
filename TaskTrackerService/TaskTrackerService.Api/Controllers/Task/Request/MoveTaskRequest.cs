namespace TaskTrackerService.Api.Controllers.Task.Request;

public record MoveTaskRequest
{
    public required int NewNumber { get; init; }
    public required Guid NewColumnId {get; init; }
}