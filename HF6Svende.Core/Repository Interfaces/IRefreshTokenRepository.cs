using HF6Svende.Core.Entities;
using HF6SvendeAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
