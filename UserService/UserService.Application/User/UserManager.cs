using Core.Errors;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserService.Application.User.Dto;

namespace UserService.Application.User;

public class UserManager : IUserManager
{
    private readonly UserManager<Domain.Entities.User>  _userManager;

    public UserManager(UserManager<Domain.Entities.User> userManager)
    {
        _userManager = userManager;
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
}