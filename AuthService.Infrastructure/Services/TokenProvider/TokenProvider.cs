using Authservice.Application.Interface;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Services.TokenProvider
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;

        public TokenProvider(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerateToken(User objuser)
        {
            var key = _config["JWT:Key"];
            var issuer = _config["JWT:Issuer"];
            var audience = _config["JWT:Audience"];
            var expireMin = _config["JWT:ExpireMinutes"];


            var claims = new List<Claim>
            {
                new Claim("UserIdentifer",objuser.Identifier ?? string.Empty),
                new Claim("UserName",objuser.UserName ?? string.Empty),
                new Claim("Role" , objuser.RoleId.ToString() ?? "0"),
                new Claim("Email",objuser.Email ?? string.Empty)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(expireMin)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
