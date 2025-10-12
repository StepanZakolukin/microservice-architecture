namespace TaskTrackerService.Dal.Models;

public class SubtaskDal : BaseDalModel<Guid>
{
    public required int SequenceNumber { get; set; }
    public required string Description { get; set; }
    public bool Completed { get; set; } = false;
    public Guid TaskId { get; set; }

    public SubtaskDal()
    {
        Id = Guid.NewGuid();
    }
}