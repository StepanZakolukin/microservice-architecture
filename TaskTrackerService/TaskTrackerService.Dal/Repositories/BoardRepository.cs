using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly ServiceDbContext _dbContext;

    public BoardRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BoardDal board, CancellationToken cancellationToken)
    {
        await _dbContext.Boards.AddAsync(board, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(BoardDal board)
    {
        _dbContext.Boards.Update(board);
    }

    public async Task<BoardDal?> GetBoardAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Boards
            .Include(board => board.Editors)
            .Include(board => board.Columns)
            .ThenInclude(column => column.Tasks)
            .Where(board => board.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Delete(BoardDal board)
    {
        _dbContext.Boards.Remove(board);
    }

    public async Task<ICollection<BoardDal>> GetBoardsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Boards
            .Where(board => board.Editors.Any(editor => editor.UserId == userId))
            .ToListAsync(cancellationToken);
    }
}