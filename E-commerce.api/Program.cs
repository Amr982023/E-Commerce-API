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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Services Registration
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

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

builder.Services.AddAuthorization(options =>
{
    // Products
    options.AddPolicy("CreateProduct", policy =>
        policy.RequireRole("Seller", "Admin"));

    options.AddPolicy("UpdateProduct", policy =>
        policy.RequireRole("Seller", "Admin"));

    options.AddPolicy("DeleteProduct", policy =>
        policy.RequireRole("Admin"));

    // Orders
    options.AddPolicy("ViewOrders", policy =>
        policy.RequireRole("Admin", "Seller"));

    options.AddPolicy("ManageOrders", policy =>
        policy.RequireRole("Admin"));


    // Users
    options.AddPolicy("ManageUsers", policy =>
        policy.RequireRole("Admin"));


});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
