namespace TaskTrackerService.Dal.Models;

public class PriorityDal : BaseDalModel<Guid>
{
    public required string Name { get; set; }
    
    public PriorityDal()
    {
        Id = Guid.NewGuid();
    }
}