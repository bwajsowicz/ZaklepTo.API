using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.Extensions;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class JwtHandler : IJwtHandler
    {
        public JwtHandler()
        {
        }

        public JwtDto CreateToken(string login, string role)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.toTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key")),
                SecurityAlgorithms.HmacSha256);
            var expiry = now.AddMinutes(20);
            var jwt = new JwtSecurityToken(
                issuer: "",
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expiry = expiry.toTimestamp()
            };
        }
    }
}
