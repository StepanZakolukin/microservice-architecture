namespace TaskTrackerService.Dal.Models;

public class ColumnDal : BaseDalModel<Guid>
{
    public required string Title { get; set; }
    public required int SequenceNumber { get; set; }
    public ICollection<TaskDal> Tasks { get; init; } = new List<TaskDal>();
    
    public ColumnDal()
    {
        Id = Guid.NewGuid();
    }
}