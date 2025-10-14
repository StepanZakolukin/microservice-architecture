namespace TaskTrackerService.Api.Controllers.Board.Request;

public record CreateColumnRequest
{
    public required string Title { get; init; }
}