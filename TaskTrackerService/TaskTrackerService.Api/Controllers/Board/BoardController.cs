using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Api.Services;

namespace TaskTrackerService.Api.Controllers.Board;

[Authorize]
[ApiController]
[Route("api/boards")]
public class BoardController : ControllerBase
{
    public BoardController()
    {
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBoardsAsync(
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}