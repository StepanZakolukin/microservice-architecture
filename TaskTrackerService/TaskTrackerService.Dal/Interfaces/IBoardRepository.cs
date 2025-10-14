using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface IBoardRepository
{
    Task AddAsync(BoardDal board, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Update(BoardDal board);
    Task<BoardDal> GetBoardAsync(Guid id, CancellationToken cancellationToken);
    void Delete(BoardDal board);
}