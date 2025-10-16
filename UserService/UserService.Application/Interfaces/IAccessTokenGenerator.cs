using System.Security.Claims;

namespace IdentityService.Api.Services.Interfaces;

public interface IAccessTokenGenerator
{
    string Generate(IEnumerable<Claim> claims);
}