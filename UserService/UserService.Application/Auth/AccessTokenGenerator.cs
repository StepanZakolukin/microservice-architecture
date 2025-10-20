using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Auth;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Auth.Interfaces;

namespace UserService.Application.Auth;

public class AccessTokenGenerator : IAccessTokenGenerator
{
    private readonly AuthOptions _authOptions;
    
    public AccessTokenGenerator(AuthOptions authOptions)
    {
        _authOptions = authOptions;
    }
    
    public string Generate(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}