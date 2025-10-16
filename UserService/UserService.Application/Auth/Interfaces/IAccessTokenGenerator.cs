using System.Security.Claims;

namespace UserService.Application.Auth.Interfaces;

public interface IAccessTokenGenerator
{
    string Generate(IEnumerable<Claim> claims);
}