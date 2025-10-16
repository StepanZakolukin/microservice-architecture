namespace UserService.Api.Controllers.User.Response;

public class NotificationResponse
{
    public required Guid Id { get; init; }
    
    public required DateTime Created { get; init; }
    
    public required string Text { get; init; }

    public required bool ReadIt { get; init; }
}