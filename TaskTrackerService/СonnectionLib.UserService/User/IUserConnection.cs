using FluentResults;

namespace СonnectionLib.UserService.User;

public interface IUserConnection
{
    public Task<Result<UserDto>> GetUserAsync(Guid userId, CancellationToken cancellationToken);
}

public class UserConnection : IUserConnection
{
    public Task<Result<UserDto>> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}