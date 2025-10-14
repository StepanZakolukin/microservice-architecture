namespace TaskTrackerService.Logic.Board.Models;

public record UpdateBoardLogic
{
    public required Guid BoardId { get; init; }
    public required string Name { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}