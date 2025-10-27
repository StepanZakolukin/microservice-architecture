using Core.Errors;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Pagination;
using UserService.Application.InterfaceRepositories;
using UserService.Application.User.Dto;

namespace UserService.Application.User;

internal class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Domain.Entities.User>  _userManager;

    public UserManager(UserManager<Domain.Entities.User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Result> UpdateUserAsync(Guid userId, UpdateUserDto dto)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        
        if (user == null)
            return Result.Fail(AppError.NotFound());

        user.Email = dto.Email;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        await _userManager.UpdateAsync(user);
        
        return Result.Ok();
    }

    public async Task<PagedList<Domain.Entities.User>> FindUsersAsync(
        UserPageQueryFilter filter,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers(cancellationToken);

        if (filter.Email != null)
            users = users.Where(user => user.NormalizedEmail!.Contains(filter.Email, StringComparison.InvariantCultureIgnoreCase));
        
        var result = users
            .OrderBy(user => user.Id)
            .ToArray();
        
        return new PagedList<Domain.Entities.User>(result, filter.Page, filter.PageSize, result.Length);
    }

    public async Task<Result<Domain.Entities.User>> GetUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        
        return user is null ? Result.Fail(AppError.NotFound()) : Result.Ok(user);
    }
}