using ExperimentsDemo.API.Behavior;
using ExperimentsDemo.API.Middleware;
using ExperimentsDemo.Application;
using ExperimentsDemo.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Appsettings & env variables

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true) // Try reading from appsettings
    .AddEnvironmentVariables(); // then override the existing env variables

#endregion

var redisConnection = builder.Configuration.GetConnectionString("Redis");

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = redisConnection;
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAppApplication();
builder.Services.AddAppInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Management API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
