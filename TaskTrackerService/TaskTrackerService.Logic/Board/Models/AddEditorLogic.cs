namespace TaskTrackerService.Logic.Board.Models;

public record AddEditorLogic
{
    public required Guid UserId { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}