using System.ComponentModel.DataAnnotations;

namespace TaskTrackerService.Api.Controllers.Task.Request;

public record MoveTaskRequest
{
    [Range(0, int.MaxValue)]
    public required int NewNumber { get; init; }
    public required Guid NewColumnId {get; init; }
}