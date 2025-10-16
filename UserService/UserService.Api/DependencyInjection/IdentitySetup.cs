using Core.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserService.Domain.Models;
using UserService.Infrastructure;

namespace UserService.Api.DependencyInjection;

public static class IdentitySetup
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = new AuthOptions();
        configuration.Bind(AuthOptions.SectionName, authOptions);
        services.AddSingleton(authOptions);
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions => ConfigureJwtOptions(jwtOptions, authOptions));
        services
            .AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ServiceDbContext>()
            .AddSignInManager<SignInManager<User>>();

        return services;
    }

    private static void ConfigureJwtOptions(JwtBearerOptions options, AuthOptions authOptions)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = authOptions.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = authOptions.Audience,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    }
}