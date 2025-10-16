namespace UserService.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Token { get; init; }

    public required Guid UserId { get; init; }
    
    public required DateTime Expires { get; init; }
    
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
    public bool IsRevoked { get; set; } = false;
    
    public bool IsActive => !IsRevoked && !IsExpired;
}