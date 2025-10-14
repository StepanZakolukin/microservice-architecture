using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface ITeamRepository
{
    Task<TeamDal?> GetTeamAsync(Guid boardId, CancellationToken cancellationToken);
}