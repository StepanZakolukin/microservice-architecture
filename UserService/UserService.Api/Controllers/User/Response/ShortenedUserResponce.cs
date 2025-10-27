namespace UserService.Api.Controllers.User.Response;

public record ShortenedUserResponce
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}