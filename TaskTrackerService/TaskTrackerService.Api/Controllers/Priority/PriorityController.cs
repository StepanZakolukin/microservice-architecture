using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Api.Controllers.Priority.Request;
using TaskTrackerService.Logic.Priority;

namespace TaskTrackerService.Api.Controllers.Priority;

[ApiController]
[Route("api/priorities")]
public class PriorityController : ControllerBase
{
    private readonly IPriorityManager _priorityManager;

    public PriorityController(IPriorityManager priorityManager)
    {
        _priorityManager = priorityManager;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePriorityAsync(
        [FromBody] PriorityRequest dto,
        CancellationToken cancellationToken)
    {
        var command = new CreatePriorityLogic
        {
            Name = dto.Name,
        };
        
        var result = await _priorityManager.CreatePriority(command, cancellationToken);
        
        return result.ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPriorityListAsync(CancellationToken cancellationToken)
    {
        var result = await _priorityManager.GetAllPrioritiesAsync(cancellationToken);
        
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{priority-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePriorityAsync(
        [FromRoute(Name = "priority-id")] Guid priorityId,
        CancellationToken cancellationToken)
    {
        var result = await _priorityManager.DeletePriorityAsync(priorityId, cancellationToken);
        return result.ToActionResult();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{priority-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePriorityAsync(
        [FromRoute(Name = "priority-id")] Guid priorityId,
        [FromBody] PriorityRequest dto,
        CancellationToken cancellationToken)
    {
        var command = new PriorityLogic
        {
            Id = priorityId,
            Name = dto.Name
        };
        
        var result = await _priorityManager.UpdatePriorityAsync(command, cancellationToken);

        return result.ToActionResult();
    }
}