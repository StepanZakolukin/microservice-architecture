namespace TaskTrackerService.Logic.Board.Models.Response;

public record TaskResponse
{
    public required Guid Id { get; init; }
    public required bool Completed { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required DateTime? Deadline { get; init; }
    public required PriorityResponse? Priority { get; init; }
}