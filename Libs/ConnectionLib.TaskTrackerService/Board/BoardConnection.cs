using FluentResults;

namespace ConnectionLib.TaskTrackerService.Board;

public class BoardConnection : IBoardConnection
{
    public Task<Result<bool>> CheckForSharedBoards(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}