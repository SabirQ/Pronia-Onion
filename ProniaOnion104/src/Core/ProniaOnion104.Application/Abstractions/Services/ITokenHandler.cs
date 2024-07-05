using ProniaOnion104.Application.DTOs.Tokens;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateJwt(AppUser user,IEnumerable<Claim> claims, int minutes);
        string CreateRefreshToken();
    }
}
