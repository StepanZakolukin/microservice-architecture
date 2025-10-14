using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface IColumnRepository
{
    Task<ColumnDal?> GetColumnAsync(Guid columnId, CancellationToken cancellationToken);
    void Update(ColumnDal column);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}