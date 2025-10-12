using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core;

public static class OpenApiSetup
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services, Assembly assembly, string appDirectory)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите JWT токен в формате: Bearer {your token}"
                });
                swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(appDirectory, xmlFile);
                swaggerGenOptions.IncludeXmlComments(xmlPath);
            });
    }
    
    
    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}