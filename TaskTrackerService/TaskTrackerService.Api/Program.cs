using System.Net;
using System.Reflection;
using System.Security.Authentication;
using Core;
using Core.Traces.Middleware;
using TaskTrackerService.Api.Services;
using TaskTrackerService.Dal;
using TaskTrackerService.Logic;
using СonnectionLib.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddAuthentication(builder.Configuration)
    .AddUserConnectionLib()
    .AddLogic()
    .AddCore(builder.Host)
    .AddDal(builder.Configuration)
    .AddOpenApi(Assembly.GetExecutingAssembly(), AppContext.BaseDirectory);;
builder.Services
    .AddScoped<IUserContext, UserContext>()
    .AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();

app.Run();