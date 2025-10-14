using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Logic.Priority;

namespace TaskTrackerService.Api.Controllers.Priority;

[ApiController]
[Route("api/priorities")]
public class PriorityController : ControllerBase
{
    private readonly IPriorityManager _manager;

    public PriorityController(IPriorityManager manager)
    {
        _manager = manager;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePriorityAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetPriorityListAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{priority-id:guid}")]
    public async Task<IActionResult> DeletePriorityAsync(
        [FromRoute(Name = "priority-id")] Guid priorityId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{priority-id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePriorityAsync(
        [FromRoute(Name = "priority-id")] Guid priorityId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{priority-id:guid}")]
    public async Task<IActionResult> GetPriorityByIdAsync(
        [FromRoute(Name = "priority-id")] Guid priorityId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}