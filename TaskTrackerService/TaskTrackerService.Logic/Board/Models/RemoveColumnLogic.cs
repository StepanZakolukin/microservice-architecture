namespace TaskTrackerService.Logic.Board.Models;

public class RemoveColumnLogic
{
    public required Guid ColumnId { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}