namespace TaskTrackerService.Logic.Board.Models.Response;

public record PriorityResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}