namespace TaskTrackerService.Dal.Models;

public class TeamDal : BaseDalModel<Guid>
{
    public required string Name { get; init; }
    public required Guid BoardId { get; init; }
    public required BoardDal Board { get; init; }
    public ICollection<TeammateDal> Teammates { get; init; } = new List<TeammateDal>();

    public TeamDal()
    {
        Id = Guid.NewGuid();
    }
}