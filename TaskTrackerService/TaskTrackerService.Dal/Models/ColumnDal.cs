namespace TaskTrackerService.Dal.Models;

public class ColumnDal : BaseDalModel<Guid>
{
    public required string Title { get; set; }
    public required int SequenceNumber { get; set; }
    public Guid BoardId { get; init; }
    public ICollection<TaskDal> Tasks { get; init; } = [];
    
    public ColumnDal()
    {
        Id = Guid.NewGuid();
    }
}