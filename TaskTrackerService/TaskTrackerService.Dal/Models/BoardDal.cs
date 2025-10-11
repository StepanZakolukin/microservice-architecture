namespace TaskTrackerService.Dal.Models;

public class BoardDal : BaseDalModel<Guid>
{
    public required string Name { get; set; }
    public required Guid TeamId { get; set; }
    public ICollection<ColumnDal> Columns { get; init; } = new List<ColumnDal>();

    public BoardDal()
    {
        Id = Guid.NewGuid();
    }
}