namespace TaskTrackerService.Logic.Priority;

public record PriorityLogic
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}