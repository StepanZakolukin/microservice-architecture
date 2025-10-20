namespace TaskTrackerService.Logic.Board.Models.Response;

public record BoardEditorResponse
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}