namespace TaskTrackerService.Logic.Task.Models;

public record UpdateTaskLogic
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required bool Completed { get; init; }
    public DateTime? Deadline { get; init; }
    public Guid PriorityId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}