using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AccountDTOs;
using E_commerce_Application.Options;
using E_commerce_Application.Services_Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace E_commerce_Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string GetRole(short role)
        {
            return role switch
            {
                1 => "Admin",
                2 => "Customer",
                3 => "Seller",
                4 => "Delivery",
                5 => "Support"
            };
        }

        public string GenerateToken(AccountDto account)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var options = _configuration.GetSection("Jwt").Get<JwtOptions>();

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                Expires = DateTime.UtcNow.AddMinutes(options.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new(ClaimTypes.NameIdentifier , account.Id.ToString()),
                    new(ClaimTypes.Name , account.UserName),
                    new(ClaimTypes.Email , account.Email),
                    new(ClaimTypes.Role , GetRole(account.UserRole)),
                    new( "UserId" , account.UserId.ToString())
                })
            };

            var Token = TokenHandler.CreateToken(TokenDescriptor);
            var jwt = TokenHandler.WriteToken(Token);
            return jwt;
        }

    }
}
