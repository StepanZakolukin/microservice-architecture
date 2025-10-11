using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface IColumnRepository
{
    void Delete(ColumnDal column);
    Task AddAsync(ColumnDal column, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Update(ColumnDal column);
}