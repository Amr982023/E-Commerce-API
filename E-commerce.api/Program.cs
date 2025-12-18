using System.Configuration;
using E_commerce_Core.Interfaces;
using E_commerce_Infrastructure;
using E_commerce_Infrastructure.DependencyInjection;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using E_commerce_Application.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;
using E_commerce.api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Application.Services;
using E_commerce_Application.Options;
using E_commerce.api.Middlewares;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Services Registration
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()              
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Host.UseSerilog();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new InvalidOperationException("JwtOptions is not configured");

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services
    .AddAuthorizationBuilder()

    // Products
    .AddPolicy("CreateProduct", policy =>
        policy.RequireRole("Seller", "Admin"))

    .AddPolicy("UpdateProduct", policy =>
        policy.RequireRole("Seller", "Admin"))

    .AddPolicy("DeleteProduct", policy =>
        policy.RequireRole("Admin"))

    // Orders
    .AddPolicy("ViewOrders", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.IsInRole("Seller") ||
            context.User.IsInRole("Customer")
        ))

    .AddPolicy("ManageOrders", policy =>
        policy.RequireRole("Admin"))

    // Users
    .AddPolicy("ManageUsers", policy =>
        policy.RequireRole("Admin"))
// Users
    .AddPolicy("ManageAccounts", policy =>
        policy.RequireRole("Admin","Seller", "Customer"));


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
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
