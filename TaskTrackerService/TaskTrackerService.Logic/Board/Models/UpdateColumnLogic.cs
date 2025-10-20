namespace TaskTrackerService.Logic.Board.Models;

public record UpdateColumnLogic
{
    public required Guid ColumnId { get; init; }
    public required string Title { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}