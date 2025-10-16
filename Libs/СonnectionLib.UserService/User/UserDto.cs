namespace СonnectionLib.UserService.User;

public record UserDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}