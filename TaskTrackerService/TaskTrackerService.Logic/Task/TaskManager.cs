using Core.Errors;
using FluentResults;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Logic.Task;

public class TaskManager : ITaskManager
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPriorityRepository _priorityRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly ITeamRepository _teamRepository;

    public TaskManager(
        ITaskRepository taskRepository,
        IColumnRepository columnRepository,
        ITeamRepository teamRepository, IPriorityRepository priorityRepository)
    {
        _taskRepository = taskRepository;
        _columnRepository = columnRepository;
        _teamRepository = teamRepository;
        _priorityRepository = priorityRepository;
    }

    public async Task<Result<Guid>> CreateTaskAsync(CreateTaskLogic dto, CancellationToken cancellationToken)
    {
        var column = await _columnRepository.GetColumnAsync(dto.ColumnId, cancellationToken);
        if (column is null)
            return Result.Fail(AppError.NotFound($"Колонка с id = {dto.ColumnId} не найдена"));

        if (!await CheckAccessRightsAsync(column.BoardId, dto.CreatorId, cancellationToken))
            return Result.Fail(AppError.Forbidden());

        var priority = default(PriorityDal);
        if (dto.PriorityId != Guid.Empty)
        {
            priority = await _priorityRepository.GetPriorityAsync(dto.PriorityId, cancellationToken);
            if (priority is null)
                return Result.Fail(AppError.NotFound($"Приоритет с id = {dto.PriorityId} не найден"));
        }

        var task = TaskDalExtension.Create(dto, priority);
        column.AddTask(task);
        _columnRepository.Update(column);
        await _columnRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(task.Id);
    }

    public async Task<Result> UpdateTaskAsync(UpdateTaskLogic dto, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskAsync(dto.Id, cancellationToken);
        if (task is null)
            return Result.Fail(AppError.NotFound());
        
        var column = await _columnRepository.GetColumnAsync(task.ColumnId, cancellationToken);

        if (!await CheckAccessRightsAsync(column!.BoardId, dto.AuthenticatedUserId, cancellationToken))
            return Result.Fail(AppError.Forbidden());
        
        PriorityDal? priority = null;
        if (dto.PriorityId != Guid.Empty)
        {
            priority = await _priorityRepository.GetPriorityAsync(dto.PriorityId, cancellationToken);
            if (priority is null)
                return Result.Fail(AppError.NotFound($"Приоритет с id = {dto.PriorityId} не найден"));
        }
        
        task.Update(dto.Title, dto.Description, dto.Completed, dto.Deadline, priority);
        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    public async Task<Result> MoveTaskAsync(MoveTaskLogic dto, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskAsync(dto.TaskId, cancellationToken);
        if (task is null)
            return Result.Fail(AppError.NotFound());
        
        var newColumn = await _columnRepository.GetColumnAsync(dto.NewColumnId, cancellationToken);
        if (newColumn is null)
            return Result.Fail(AppError.NotFound($"Колонка с id = {dto.NewColumnId} не найдена"));
        
        if (!await CheckAccessRightsAsync(newColumn.BoardId, dto.AuthenticatedUserId, cancellationToken))
            return Result.Fail(AppError.Forbidden());
        
        var oldColumn = newColumn;
        if (task.ColumnId != dto.NewColumnId)
        {
            oldColumn = await _columnRepository.GetColumnAsync(task.ColumnId, cancellationToken);
            if (dto.NewNumber > newColumn.Count)
                return Result.Fail(AppError.Validation($"{nameof(dto.NewNumber)} должен быть меньше или равен {newColumn.Count}"));
        }
        else if (dto.NewNumber >= newColumn.Count)
        {
            return Result.Fail(AppError.Validation($"{nameof(dto.NewNumber)} должен быть меньше {newColumn.Count}"));
        }

        if (newColumn.BoardId != oldColumn!.BoardId)
            return Result.Fail(AppError.Validation("Задачу можно переносить только между колонками одной доски"));
        
        oldColumn.RemoveTask(task);
        _columnRepository.Update(oldColumn);
        newColumn.AddTask(task, dto.NewNumber);
        _columnRepository.Update(newColumn);
        await _columnRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> DeleteTaskAsync(DeleteTaskLogic dto, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskAsync(dto.TaskId, cancellationToken);
        if (task is null)
            return Result.Fail(AppError.NotFound());
        
        var column = await _columnRepository.GetColumnAsync(task.ColumnId, cancellationToken);

        if (!await CheckAccessRightsAsync(column!.BoardId, dto.AuthenticatedUserId, cancellationToken))
            return Result.Fail(AppError.Forbidden());
        
        _taskRepository.Delete(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    private async Task<bool> CheckAccessRightsAsync(Guid boardId, Guid userId, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetTeamAsync(boardId, cancellationToken);
        
        return team!.Teammates.Any(teammate => teammate.UserId == userId);
    }
}