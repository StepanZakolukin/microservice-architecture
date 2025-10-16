using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Models;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public DateTime CreateAt { get; init; } = DateTime.UtcNow;
}