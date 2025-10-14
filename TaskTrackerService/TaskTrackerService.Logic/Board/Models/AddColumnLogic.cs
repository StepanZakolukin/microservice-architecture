namespace TaskTrackerService.Logic.Board.Models;

public record AddColumnLogic
{
    public required string Title { get; init; }
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}