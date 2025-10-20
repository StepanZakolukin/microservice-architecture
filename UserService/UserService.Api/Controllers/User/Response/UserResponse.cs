namespace UserService.Api.Controllers.User.Response;

public record UserResponse
{
    public required string Email { get; init; }
    public required string LastName { get; init; }
    public required string FirstName { get; init; }
    public required DateTime CreateAt {get; init; }
}