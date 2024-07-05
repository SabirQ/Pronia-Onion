using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Tokens;
using ProniaOnion104.Application.DTOs.Users;
using ProniaOnion104.Domain.Entities;
using ProniaOnion104.Domain.Enums;
using System.Security.Claims;
using System.Text;

namespace ProniaOnion104.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly ITokenHandler _handler;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationService(IMapper mapper,ITokenHandler handler, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _handler = handler;
            _userManager = userManager;
        }

       
        public async Task Register(RegisterDto dto)
        {

           AppUser user=await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName || u.Email == dto.Email);
            if (user is not null) throw new Exception("same");
           user=_mapper.Map<AppUser>(dto);

           var result= await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder message = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    message.AppendLine(error.Description);
                }
                
                throw new Exception(message.ToString());
            }
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());

        }
        public async Task<TokenResponseDto> Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
                if (user is null) throw new Exception("Username, Email or Password incorrect");
            }
            if (!await _userManager.CheckPasswordAsync(user, dto.Password)) throw new Exception("Username, Email or Password incorrect");

            ICollection<Claim> claims = await _createClaims(user);

            TokenResponseDto tokenDto = _handler.CreateJwt(user, claims, 1);
            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpiredAt = tokenDto.RefreshExpireTime;
            await _userManager.UpdateAsync(user);

            return tokenDto;

        }

        private async Task<ICollection<Claim>> _createClaims(AppUser user)
        {
            ICollection<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.GivenName,user.Name),
            new Claim(ClaimTypes.Surname,user.Surname)
            };
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<TokenResponseDto> LoginByRefreshToken(string refresh)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refresh);
            if (user is null) throw new Exception("Not found");
            if (user.RefreshTokenExpiredAt<DateTime.UtcNow) throw new Exception("Expired");

           TokenResponseDto tokenDto= _handler.CreateJwt(user, await _createClaims(user), 60);

            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpiredAt = tokenDto.RefreshExpireTime;
            await _userManager.UpdateAsync(user);

            return tokenDto;

        }



    }
}
