using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Api.Controllers.Board.Request;
using TaskTrackerService.Api.Services;
using TaskTrackerService.Logic.Board;
using TaskTrackerService.Logic.Board.Models;
using TaskTrackerService.Logic.Board.Models.Response;

namespace TaskTrackerService.Api.Controllers.Board;

[Authorize]
[ApiController]
[Route("api/boards")]
public class BoardController : ControllerBase
{
    private readonly IBoardManager _boardManager;
    public BoardController(IBoardManager boardManager)
    {
        _boardManager = boardManager;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<ShortenedBoardResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBoardsAsync(
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var result = await _boardManager.GetBoardListAsync(userContext.UserId, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateBoardAsync(
        [FromBody] BoardRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new CreateBoardLogic
        {
            Name = dto.Name,
            AuthenticatedUserId = userContext.UserId,
        };
        
        var result = await _boardManager.CreateBoardAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{board-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBoardAsync(
        [FromRoute(Name = "board-id")] Guid boardId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken )
    {
        var command = new DeleteBoardLogic
        {
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId,
        };
        
        var result = await _boardManager.DeleteBoardAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPatch("{board-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBoardAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromBody] BoardRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBoardLogic
        {
            Name = dto.Name,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };

        var result = await _boardManager.UpdateBoardAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("{board-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BoardResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBoardAsync(
        [FromRoute(Name = "board-id")] Guid boardId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new GetBoardLogic
        {
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };
        
        var result = await _boardManager.GetBoardAsync(command, cancellationToken);

        return result.ToActionResult();
    }


    [HttpPost("{board-id:guid}/editors")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddEditorAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromBody] CreateEditorRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new AddEditorLogic
        {
            UserId = dto.UserId,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };
        
        var result = await _boardManager.AddEditorAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{board-id:guid}/editors/{editor-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveEditorAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromRoute(Name = "editor-id")] Guid editorId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new AddEditorLogic
        {
            UserId = editorId,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };
        
        var result = await _boardManager.RemoveEditorAsync(command, cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpPost("{board-id:guid}/columns")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddColumnAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromBody] CreateColumnRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new AddColumnLogic
        {
            Title = dto.Title,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };
        
        var result = await _boardManager.AddColumnAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{board-id:guid}/columns/{column-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveColumnAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromRoute(Name = "column-id")] Guid columnId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new RemoveColumnLogic
        {
            ColumnId = columnId,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };

        var result = await _boardManager.RemoveColumnAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPatch("{board-id:guid}/columns/{column-id:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateColumnAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromRoute(Name = "column-id")] Guid columnId,
        [FromBody] UpateColumnRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new UpdateColumnLogic
        {
            ColumnId = columnId,
            Title = dto.Title,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };

        var result = await _boardManager.UpdateColumnAsync(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPost("{board-id:guid}/columns/{column-id:guid}/move")]
    public async Task<IActionResult> MoveColumnAsync(
        [FromRoute(Name = "board-id")] Guid boardId, 
        [FromRoute(Name = "column-id")] Guid columnId,
        [FromBody] MoveColumnRequest dto,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var command = new MoveColumnLogic
        {
            ColumnId = columnId,
            NewNumber = dto.NewNumber,
            BoardId = boardId,
            AuthenticatedUserId = userContext.UserId
        };

        var result = await _boardManager.MoveColumnAsync(command, cancellationToken);
        
        return result.ToActionResult();
    }
}