using HF6Svende.Application.DTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string email, string? role, string userName, string userId);
        ClaimsPrincipal? ValidateToken(string token);

    }
}
