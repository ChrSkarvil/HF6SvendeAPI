using HF6Svende.Core.Entities;
using HF6Svende.Core.Repository_Interfaces;
using HF6SvendeAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public RefreshTokenRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }

        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the refresh token.", ex);
            }
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == token);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the refresh token.", ex);
            }
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the refresh token.", ex);
            }
        }
    }
}
