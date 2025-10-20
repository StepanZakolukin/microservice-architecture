using Core.Errors;
using FluentResults;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Board.Models;
using TaskTrackerService.Logic.Board.Models.Response;
using СonnectionLib.UserService.User;

namespace TaskTrackerService.Logic.Board;

public class BoardManager : IBoardManager
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserConnection _userConnection;

    public BoardManager(IBoardRepository boardRepository, IUserConnection userConnection)
    {
        _boardRepository = boardRepository;
        _userConnection = userConnection;
    }

    public async Task<ICollection<ShortenedBoardResponse>> GetBoardListAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var boards = await _boardRepository.GetBoardsAsync(userId, cancellationToken);

        return boards.Select(board => new ShortenedBoardResponse { Id = board.Id, Name = board.Name }).ToList();
    }

    public async Task<Result<Guid>> CreateBoardAsync(CreateBoardLogic dto, CancellationToken cancellationToken)
    {
        var userResult = await _userConnection.GetUserAsync(dto.AuthenticatedUserId, cancellationToken);
        if (userResult.IsFailed)
            return Result.Fail("Что то пошло не так, попробуйте повторить попытку");
        var user = userResult.Value;
        
        var board = new BoardDal
        {
            Name = dto.Name
        };
        board.AddEditor(user.Id, user.FirstName, user.LastName);
        await _boardRepository.AddAsync(board, cancellationToken);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(board.Id);
    }

    public async Task<Result> DeleteBoardAsync(DeleteBoardLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        _boardRepository.Delete(board);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> UpdateBoardAsync(UpdateBoardLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());

        board.Name = dto.Name;
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result<BoardResponse>> GetBoardAsync(GetBoardLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        var boardDto = new BoardResponse
        {
            Id = board.Id,
            Name = board.Name,
            Editors = board.Editors.Select(editor => editor.ConvertToBoardEditorResponse()).ToArray(),
            Columns = board.Columns.Select(column => column.ConvertToColumnResponse()).ToArray()
        };
        
        return Result.Ok(boardDto);
    }

    public async Task<Result<Guid>> AddEditorAsync(AddEditorLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        var userResult = await _userConnection.GetUserAsync(dto.UserId, cancellationToken);
        if (userResult.IsFailed)
            return Result.Fail("Что то пошло не так, попробуйте повторить попытку");
        var user = userResult.Value;

        var editor = board.AddEditor(user.Id, user.FirstName, user.LastName);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(editor.Id);
    }

    public async Task<Result> RemoveEditorAsync(AddEditorLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        board.RemoveEditor(dto.UserId);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result<Guid>> AddColumnAsync(AddColumnLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());

        var column = board.AddColumn(dto.Title);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(column.Id);
    }

    public async Task<Result> RemoveColumnAsync(RemoveColumnLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());

        board.RemoveColumn(dto.ColumnId);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> UpdateColumnAsync(UpdateColumnLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());

        var column = board.GetColumn(dto.ColumnId);
        if (column is null)
            return Result.Fail(AppError.NotFound("Колонка не найдена"));
        
        column.Title = dto.Title;
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> MoveColumnAsync(MoveColumnLogic dto, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(dto.BoardId, cancellationToken);
        
        if (board is null)
            return Result.Fail(AppError.NotFound("Доска не найдена"));
        
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());

        if (!board.TryMoveColumn(dto.ColumnId, dto.NewNumber))
            return Result.Fail(AppError.Validation());
        
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}