namespace TaskTrackerService.Logic.Board.Models.Response;

public record BoardResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required ICollection<BoardEditorResponse> Editors { get; init; }
    public required ICollection<ColumnResponse> Columns { get; init; }
}