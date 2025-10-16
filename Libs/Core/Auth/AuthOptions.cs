using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Auth;

public class AuthOptions
{
    public const string SectionName = "JWT";
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }

    public string Key { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}