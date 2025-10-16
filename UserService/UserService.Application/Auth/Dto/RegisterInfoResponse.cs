namespace UserService.Application.Auth.Dto;

public record RegisterInfoResponse
{
    public required Guid UserId { get; init; }
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}