using System.ComponentModel.DataAnnotations;

namespace TaskTrackerService.Api.Controllers.Board.Request;

public record MoveColumnRequest
{
    [Range(0, int.MaxValue)]
    public required int NewNumber { get; init; }
}