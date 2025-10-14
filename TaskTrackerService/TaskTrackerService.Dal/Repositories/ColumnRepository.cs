using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

internal class ColumnRepository : IColumnRepository
{
    private readonly ServiceDbContext _dbContext;

    public ColumnRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ColumnDal?> GetColumnAsync(Guid columnId, CancellationToken cancellationToken)
    {
        return await _dbContext.Columns
            .Include(column => column.Tasks)
            .Where(column => column.Id == columnId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(ColumnDal column)
    {
        _dbContext.Columns.Update(column);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}