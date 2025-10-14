using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Api.Controllers.Task.Request;
using TaskTrackerService.Api.Services;
using TaskTrackerService.Logic.Task;
using TaskTrackerService.Logic.Task.Models;

namespace TaskTrackerService.Api.Controllers.Task;

[Authorize]
[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskManager _taskManager;
    public TaskController(ITaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTaskAsync(
        [FromBody] CreateTaskRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var task = new CreateTaskLogic
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatorId = userContext.UserId,
            Deadline = dto.Deadline,
            ColumnId = dto.ColumnId,
            PriorityId = dto.PriorityId ?? Guid.Empty
        };
        var result = await _taskManager.CreateTaskAsync(task, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPatch("{task-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTaskAsync(
        [FromRoute(Name = "task-id")] Guid taskId,
        [FromBody] UpdateTaskRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var task = new UpdateTaskLogic
        {
            Id = taskId,
            Title = dto.Title,
            Description = dto.Description,
            Completed = dto.Completed,
            AuthenticatedUserId = userContext.UserId,
        };
        var result = await _taskManager.UpdateTaskAsync(task, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost("{task-id:guid}/move")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> MoveTaskAsync(
        [FromRoute(Name = "task-id")] Guid taskId,
        [FromBody] MoveTaskRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new MoveTaskLogic
        {
            TaskId = taskId,
            NewNumber = dto.NewNumber,
            NewColumnId = dto.NewColumnId,
            AuthenticatedUserId = userContext.UserId
        };
        var result = await _taskManager.MoveTaskAsync(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{task-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteTaskAsync(
        [FromRoute(Name = "task-id")] Guid taskId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTaskLogic
        {
            TaskId = taskId,
            AuthenticatedUserId = userContext.UserId
        };
        var result = await _taskManager.DeleteTaskAsync(command, cancellationToken);
        return result.ToActionResult();
    }
}