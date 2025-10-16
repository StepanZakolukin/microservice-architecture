using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Controllers.User.Request;

public record RegisterInfoRequest
{
    [MaxLength(255, ErrorMessage = "First name cannot be longer than 255 characters.")]
    public required string FirstName { get; init; }
    
    [MaxLength(255, ErrorMessage = "Last name cannot be longer than 255 characters.")]
    public required string LastName { get; init; }
    
    [EmailAddress]
    public required string Email { get; init; }
    
    [DataType(DataType.Password)]
    public required string Password { get; init; }
    
    [DataType(DataType.Password)]
    [Compare($"{nameof(Password)}", ErrorMessage = "Пароли не совпадают")]
    public required string PasswordConfirm { get; init; }
}