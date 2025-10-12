using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Errors;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        var error = result.Errors.OfType<AppError>().FirstOrDefault()
                    ?? result.Errors.FirstOrDefault() as AppError
                    ?? new AppError("Unknown", result.Errors.First().Message);

        return ConvertToActionResult(error);
    }

    private static IActionResult ConvertToActionResult(AppError error)
    {
        var status = error.Code switch
        {
            "Validation" => StatusCodes.Status400BadRequest,
            "Unauthorized" => StatusCodes.Status401Unauthorized,
            "NotFound"   => StatusCodes.Status404NotFound,
            "Conflict"   => StatusCodes.Status409Conflict,
            _            => StatusCodes.Status500InternalServerError
        };

        var details = new ProblemDetails
        {
            Status = status,
            Title = error.Code,
            Detail = error.Message,
            Type = $"https://httpstatuses.com/{status}"
        };

        return new ObjectResult(details) { StatusCode = status };
    }
    
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        var error = result.Errors.OfType<AppError>().FirstOrDefault()
                    ?? result.Errors.FirstOrDefault() as AppError
                    ?? new AppError("Unknown", result.Errors.First().Message);
        
        return ConvertToActionResult(error);
    }
}