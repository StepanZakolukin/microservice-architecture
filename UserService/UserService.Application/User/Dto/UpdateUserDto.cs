using System.ComponentModel.DataAnnotations;

namespace UserService.Application.User.Dto;

public record UpdateUserDto
{
    [MaxLength(255, ErrorMessage = "First name cannot be longer than 255 characters.")]
    public required string FirstName { get; init; }
    [MaxLength(255, ErrorMessage = "Last name cannot be longer than 255 characters.")]
    public required string LastName { get; init; }
    [EmailAddress]
    public required string Email { get; init; }
}