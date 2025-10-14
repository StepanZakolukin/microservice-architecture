using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Board.Models.Response;

namespace TaskTrackerService.Logic;

public static class BoardEditorDalExtension
{
    public static BoardEditorResponse ConvertToBoardEditorResponse(this BoardEditorDal editor)
    {
        return new BoardEditorResponse
        {
            Id = editor.Id,
            UserId = editor.UserId,
            FirstName = editor.FirstName,
            LastName = editor.LastName
        };
    }
}