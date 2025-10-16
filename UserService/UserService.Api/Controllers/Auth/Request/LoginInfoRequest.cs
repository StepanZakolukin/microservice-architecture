using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Controllers.User.Request;

public record LoginInfoRequest
{
    [EmailAddress]
    public required string Email { get; init; }
    
    
    [DataType(DataType.Password)]
    public required string Password { get; init; }
}