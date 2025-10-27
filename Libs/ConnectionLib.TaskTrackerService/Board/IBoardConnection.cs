using FluentResults;

namespace ConnectionLib.TaskTrackerService.Board;

public interface IBoardConnection
{
    /// <summary>
    /// Проверяет наличие общих досок у двух пользователей 
    /// </summary>
    Task<Result<bool>> CheckForSharedBoards(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken);
}