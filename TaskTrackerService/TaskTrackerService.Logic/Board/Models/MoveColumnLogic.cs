namespace TaskTrackerService.Logic.Board.Models;

public record MoveColumnLogic
{
    public required Guid ColumnId { get; init; }
    public required int NewNumber { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}