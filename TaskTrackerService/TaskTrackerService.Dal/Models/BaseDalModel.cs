namespace TaskTrackerService.Dal.Models;

public abstract class BaseDalModel<TKey>
{
    public TKey Id { get; init; }
}