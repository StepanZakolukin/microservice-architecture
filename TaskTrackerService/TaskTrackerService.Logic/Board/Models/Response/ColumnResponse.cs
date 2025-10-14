namespace TaskTrackerService.Logic.Board.Models.Response;

public record ColumnResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required ICollection<TaskResponse> Tasks { get; init; }
}