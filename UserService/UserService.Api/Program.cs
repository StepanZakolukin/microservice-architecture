using Core;
using UserService.Api.DependencyInjection;
using UserService.Application;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddIdentityServices(builder.Configuration)
    .AddCore(builder.Host)
    .AddDal(builder.Configuration)
    .AddLogic()
    .AddOpenApi(typeof(Program).Assembly, AppContext.BaseDirectory);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();