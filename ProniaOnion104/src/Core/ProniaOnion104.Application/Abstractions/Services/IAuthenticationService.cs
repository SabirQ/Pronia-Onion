using ProniaOnion104.Application.DTOs.Tokens;
using ProniaOnion104.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task Register(RegisterDto dto);
        Task<TokenResponseDto> Login(LoginDto dto);
        Task<TokenResponseDto> LoginByRefreshToken(string refresh);
    }
}
