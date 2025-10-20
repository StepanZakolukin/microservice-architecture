namespace TaskTrackerService.Api.Controllers.Board.Request;

public record UpateColumnRequest
{
    public required string Title { get; init; } 
}