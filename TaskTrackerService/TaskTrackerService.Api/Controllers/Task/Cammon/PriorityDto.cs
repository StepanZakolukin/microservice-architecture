namespace TaskTrackerService.Api.Controllers.Task.Cammon;

public record PriorityDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}