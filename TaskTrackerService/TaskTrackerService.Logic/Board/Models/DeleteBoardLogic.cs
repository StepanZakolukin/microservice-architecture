namespace TaskTrackerService.Logic.Board.Models;

public record DeleteBoardLogic
{
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}