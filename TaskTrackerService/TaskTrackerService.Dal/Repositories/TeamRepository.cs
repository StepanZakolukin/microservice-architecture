using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ServiceDbContext _dbContext;

    public TeamRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TeamDal?> GetTeamAsync(Guid boardId, CancellationToken cancellationToken)
    {
        IQueryable<TeamDal> teams = _dbContext.Teams;
        return await teams.Where(team => team.BoardId == boardId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}