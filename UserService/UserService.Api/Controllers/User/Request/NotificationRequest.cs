namespace UserService.Api.Controllers.User.Request;

public record NotificationRequest
{
    public required string Text { get; init; }
}