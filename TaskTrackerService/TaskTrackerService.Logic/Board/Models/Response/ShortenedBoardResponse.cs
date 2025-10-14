namespace TaskTrackerService.Logic.Board.Models.Response;

public record ShortenedBoardResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}