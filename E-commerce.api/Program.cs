using System.Configuration;
using E_commerce_Core.Interfaces;
using E_commerce_Infrastructure;
using E_commerce_Infrastructure.DependencyInjection;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using E_commerce_Application.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Services Registration
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
// Rate limiter configuration
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 5;
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
