using Core.Errors;
using FluentResults;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Logic.Priority;

public class PriorityManager : IPriorityManager
{
    private readonly IPriorityRepository _priorityRepository;

    public PriorityManager(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public async Task<Result<Guid>> CreatePriority(CreatePriorityLogic dto, CancellationToken cancellationToken)
    {
        var priority = new PriorityDal { Name = dto.Name };
        
        await _priorityRepository.AddAsync(priority, cancellationToken);
        
        return Result.Ok(priority.Id);
    }

    public async Task<ICollection<PriorityLogic>> GetAllPrioritiesAsync(CancellationToken cancellationToken)
    {
        var priorityList = await _priorityRepository.GetAllAsync(cancellationToken);
        return priorityList
            .Select(priority => new PriorityLogic { Id = priority.Id, Name = priority.Name })
            .ToList();
    }

    public async Task<Result> DeletePriorityAsync(Guid priorityId, CancellationToken cancellationToken)
    {
        var priority = await _priorityRepository.GetPriorityAsync(priorityId, cancellationToken);
        if (priority is null)
            return Result.Fail(AppError.NotFound());
        
        _priorityRepository.Delete(priority);
        _priorityRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> UpdatePriorityAsync(PriorityLogic dto, CancellationToken cancellationToken)
    {
        var priority = await _priorityRepository.GetPriorityAsync(dto.Id, cancellationToken);
        if (priority is null)
            return Result.Fail(AppError.NotFound());
        
        priority.Name = dto.Name;
        _priorityRepository.Update(priority);
        _priorityRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}