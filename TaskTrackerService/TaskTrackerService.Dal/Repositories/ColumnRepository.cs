using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly ServiceDbContext _dbContext;

    public ColumnRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ColumnDal?> GetColumnAsync(Guid columnId, CancellationToken cancellationToken)
    {
        IQueryable<ColumnDal> columns = _dbContext.Columns;
        return await columns
            .Where(column => column.Id == columnId)
            .Include(column => column.Tasks)
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