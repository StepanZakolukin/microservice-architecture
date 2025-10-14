namespace TaskTrackerService.Logic.Board.Models;

public record GetBoardLogic
{
    public required Guid BoardId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}