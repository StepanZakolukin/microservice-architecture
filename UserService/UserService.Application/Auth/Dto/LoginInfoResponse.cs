namespace UserService.Application.Auth.Dto;

public record LoginInfoResponse
{
    public required string AccessToken { get; init; }
    
    public required string RefreshToken { get; init; }
};