using FluentResults;
using Saritasa.Tools.Common.Pagination;
using UserService.Application.User.Dto;

namespace UserService.Application.User;

public interface IUserManager
{
    Task<Result> UpdateUserAsync(Guid userId, UpdateUserDto dto);

    Task<PagedList<Domain.Entities.User>> FindUsersAsync(
        UserPageQueryFilter filter,
        CancellationToken cancellationToken);

    Task<Result<Domain.Entities.User>> GetUserAsync(Guid userId);
}