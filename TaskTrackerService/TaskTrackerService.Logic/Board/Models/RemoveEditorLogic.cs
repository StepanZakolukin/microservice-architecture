namespace TaskTrackerService.Logic.Board.Models;

public record RemoveEditorLogic
{
    public required Guid EditorId { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}