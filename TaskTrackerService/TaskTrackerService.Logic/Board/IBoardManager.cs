using FluentResults;
using TaskTrackerService.Logic.Board.Models;
using TaskTrackerService.Logic.Board.Models.Response;

namespace TaskTrackerService.Logic.Board;

public interface IBoardManager
{
    Task<ICollection<ShortenedBoardResponse>> GetBoardListAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateBoardAsync(CreateBoardLogic dto, CancellationToken cancellationToken);
    Task<Result> DeleteBoardAsync(DeleteBoardLogic dto, CancellationToken cancellationToken);
    Task<Result> UpdateBoardAsync(UpdateBoardLogic dto, CancellationToken cancellationToken);
    Task<Result<BoardResponse>> GetBoardAsync(GetBoardLogic dto, CancellationToken cancellationToken);
    Task<Result<Guid>> AddEditorAsync(AddEditorLogic dto, CancellationToken cancellationToken);
    Task<Result> RemoveEditorAsync(AddEditorLogic dto, CancellationToken cancellationToken);
    Task<Result<Guid>> AddColumnAsync(AddColumnLogic dto, CancellationToken cancellationToken);
    Task<Result> RemoveColumnAsync(RemoveColumnLogic dto, CancellationToken cancellationToken);
    Task<Result> UpdateColumnAsync(UpdateColumnLogic dto, CancellationToken cancellationToken);
    Task<Result> MoveColumnAsync(MoveColumnLogic dto, CancellationToken cancellationToken);
}