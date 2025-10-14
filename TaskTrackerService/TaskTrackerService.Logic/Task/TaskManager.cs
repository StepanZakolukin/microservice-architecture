using Core.Errors;
using FluentResults;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Logic.Task;

internal class TaskManager : ITaskManager
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPriorityRepository _priorityRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly IBoardRepository _boardRepository;

    public TaskManager(
        ITaskRepository taskRepository,
        IColumnRepository columnRepository,
        IPriorityRepository priorityRepository, IBoardRepository boardRepository)
    {
        _taskRepository = taskRepository;
        _columnRepository = columnRepository;
        _priorityRepository = priorityRepository;
        _boardRepository = boardRepository;
    }

    public async Task<Result<Guid>> CreateTaskAsync(CreateTaskLogic dto, CancellationToken cancellationToken)
    {
        var column = await _columnRepository.GetColumnAsync(dto.ColumnId, cancellationToken);
        if (column is null)
            return Result.Fail(AppError.NotFound($"Колонка с id = {dto.ColumnId} не найдена"));

        var board = await _boardRepository.GetBoardAsync(column.BoardId, cancellationToken);
        if (!board.CheckEditorExists(dto.CreatorId))
            return Result.Fail(AppError.Forbidden());

        var priority = default(PriorityDal);
        if (dto.PriorityId != Guid.Empty)
        {
            priority = await _priorityRepository.GetPriorityAsync(dto.PriorityId, cancellationToken);
            if (priority is null)
            {
                return Result.Fail(AppError.NotFound($"Приоритет с id = {dto.PriorityId} не найден"));
            }
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

        var board = await _boardRepository.GetBoardAsync(task.Column.BoardId, cancellationToken);
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        PriorityDal? priority = null;
        if (dto.PriorityId != Guid.Empty)
        {
            priority = await _priorityRepository.GetPriorityAsync(dto.PriorityId, cancellationToken);
            if (priority is null)
            {
                return Result.Fail(AppError.NotFound($"Приоритет с id = {dto.PriorityId} не найден"));
            }
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
        
        var board = await _boardRepository.GetBoardAsync(task.Column.BoardId, cancellationToken);
        if (board.Editors.All(editor => editor.Id != dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        var newColumn = board.Columns.FirstOrDefault(column => column.Id == dto.NewColumnId);
        if (newColumn is null)
            return Result.Fail(AppError.Validation("Задачу можно переносить только между колонками одной доски"));
        
        if (!board.TryMoveTask(task, newColumn, dto.NewNumber))
            return Result.Fail(AppError.Validation($"Передано невалидное значение для {nameof(dto.NewNumber)}"));
        
        _boardRepository.Update(board);
        await _boardRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result> DeleteTaskAsync(DeleteTaskLogic dto, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskAsync(dto.TaskId, cancellationToken);
        if (task is null)
            return Result.Fail(AppError.NotFound());
        
        var board = await _boardRepository.GetBoardAsync(task.Column.BoardId, cancellationToken);
        if (!board.CheckEditorExists(dto.AuthenticatedUserId))
            return Result.Fail(AppError.Forbidden());
        
        _taskRepository.Delete(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}