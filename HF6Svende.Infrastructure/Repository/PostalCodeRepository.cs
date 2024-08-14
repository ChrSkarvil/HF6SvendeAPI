using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Core.Repository_Interfaces;
using HF6SvendeAPI.Data;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HF6Svende.Infrastructure.Repository
{
    public class PostalCodeRepository : IPostalCodeRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public PostalCodeRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<List<PostalCode>> GetAllPostalCodesAsync()
        {
            try
            {
                return await _context.PostalCodes.Include(l => l.Country).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the postalcodes.", ex);
            }
        }

        public async Task<PostalCode?> GetPostalCodeByCodeAsync(string postalCode)
        {
            try
            {
                return await _context.PostalCodes.Include(l => l.Country).FirstOrDefaultAsync(l => l.PostCode == postalCode);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the postalcode.", ex);
            }
        }
    }
}
