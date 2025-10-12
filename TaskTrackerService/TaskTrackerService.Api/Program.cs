using System.Reflection;
using Core;
using TaskTrackerService.Api.Services;
using TaskTrackerService.Dal;
using TaskTrackerService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddLogic()
    .AddCore(builder.Host)
    .AddNotifications()
    .AddDal(builder.Configuration)
    .AddOpenApi(Assembly.GetExecutingAssembly(), AppContext.BaseDirectory);;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseNotifications();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();