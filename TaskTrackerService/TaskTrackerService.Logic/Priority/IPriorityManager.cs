using FluentResults;

namespace TaskTrackerService.Logic.Priority;

public interface IPriorityManager
{
    Task<Result<Guid>> CreatePriority(CreatePriorityLogic dto, CancellationToken cancellationToken);
    Task<ICollection<PriorityLogic>> GetAllPrioritiesAsync(CancellationToken cancellationToken);
    Task<Result> DeletePriorityAsync(Guid priorityId, CancellationToken cancellationToken);
    Task<Result> UpdatePriorityAsync(PriorityLogic dto, CancellationToken cancellationToken);
}