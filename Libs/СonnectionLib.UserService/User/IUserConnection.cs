using FluentResults;

namespace СonnectionLib.UserService.User;

public interface IUserConnection
{
    Task<Result<UserDto>> GetUserAsync(Guid userId, CancellationToken cancellationToken);
}