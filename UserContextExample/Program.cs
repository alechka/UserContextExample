using Scalar.AspNetCore;
using UserContextExample;
using UserContextExample.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<Layer2Service>();
builder.Services.AddScoped<Layer3Service>();
builder.Services.AddScoped<UserContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.UseMiddleware<UserContextMiddleware>();

app.MapControllers();

app.Run();
