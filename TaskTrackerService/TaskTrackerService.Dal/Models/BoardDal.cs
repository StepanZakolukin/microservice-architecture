namespace TaskTrackerService.Dal.Models;

public class BoardDal : BaseDalModel<Guid>
{
    public required string Name { get; set; }
    public required Guid TeamId { get; set; }
    public required TeamDal Team { get; init; }
    public ICollection<ColumnDal> Columns { get; init; } = [];

    public BoardDal()
    {
        Id = Guid.NewGuid();
    }
}