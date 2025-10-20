namespace UserService.Application.InterfaceRepositories;

public interface IUserRepository
{
    Task<Domain.Entities.User?> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
    
    Task<IEnumerable<Domain.Entities.User>> GetAllUsers(CancellationToken cancellationToken);
}