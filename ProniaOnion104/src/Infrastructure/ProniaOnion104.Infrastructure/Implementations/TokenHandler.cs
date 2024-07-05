using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Tokens;
using ProniaOnion104.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProniaOnion104.Infrastructure.Implementations
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }

        public TokenResponseDto CreateJwt(AppUser user,IEnumerable<Claim> claims, int minutes)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));
            SigningCredentials signing = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: signing
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenResponseDto dto = new TokenResponseDto(handler.WriteToken(token), token.ValidTo, user.UserName,CreateRefreshToken(),token.ValidTo.AddMinutes(1));
            return dto;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();

            //byte[] bytes = new byte[32];
            //var random =RandomNumberGenerator.Create();
            //random.GetBytes(bytes);
            //return Convert.ToBase64String(bytes);
        }
    }
}
