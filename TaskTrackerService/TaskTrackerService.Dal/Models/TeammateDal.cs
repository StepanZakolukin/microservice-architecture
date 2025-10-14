namespace TaskTrackerService.Dal.Models;

public class TeammateDal : BaseDalModel<Guid>
{
    public required Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    
    public TeammateDal()
    {
        Id = Guid.NewGuid();
    }
}