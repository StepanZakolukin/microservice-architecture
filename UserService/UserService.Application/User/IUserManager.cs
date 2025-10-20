using FluentResults;
using UserService.Application.User.Dto;

namespace UserService.Application.User;

public interface IUserManager
{
    Task<Result> UpdateUserAsync(Guid userId, UpdateUserDto dto);
}