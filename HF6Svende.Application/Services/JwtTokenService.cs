using Azure;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.Service_Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenService(IConfiguration configuration)
        {
            _secretKey = configuration["JwtConfig:SecretKey"];
            _issuer = configuration["JwtConfig:JwtIssuer"];
            _audience = configuration["JwtConfig:JwtAudience"];
        }

        public string GenerateJwtToken(string email, string? role, string userName, string userId, int? customerId)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_secretKey);
                var tokenHandler = new JwtSecurityTokenHandler();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role ?? string.Empty),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                };

                if (customerId.HasValue)
                {
                    claims.Add(new Claim("CustomerId", customerId.Value.ToString()));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _issuer,
                    Audience = _audience
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating token.", ex);
            }

        }

        public string GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating refresh token.", ex);
            }
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
                ValidateLifetime = false // Ignore token expiration
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            else
            {
                throw new SecurityTokenException("Invalid token.");
            }
        }
    }
}
