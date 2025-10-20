namespace TaskTrackerService.Api.Controllers.Board.Request;

public record BoardRequest
{
    public required string Name { get; init; }
}