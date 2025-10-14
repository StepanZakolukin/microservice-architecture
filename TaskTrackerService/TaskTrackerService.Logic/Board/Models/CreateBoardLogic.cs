namespace TaskTrackerService.Logic.Board.Models;

public record CreateBoardLogic
{
    public required string Name { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}