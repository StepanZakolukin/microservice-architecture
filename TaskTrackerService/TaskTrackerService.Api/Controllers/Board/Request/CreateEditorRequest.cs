namespace TaskTrackerService.Api.Controllers.Board.Request;

public record CreateEditorRequest
{
    public required Guid UserId { get; init; }
}