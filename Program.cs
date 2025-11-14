using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementAPI.Services;
using UserManagementAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register in-memory user service
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Configure middleware pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<TokenAuthMiddleware>(); // Authentication
app.UseMiddleware<RequestLoggingMiddleware>(); // Logging

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
