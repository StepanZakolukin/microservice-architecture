namespace TaskTrackerService.Api.Controllers.Priority.Request;

public record PriorityRequest
{
    public required string Name { get; init; }
}
