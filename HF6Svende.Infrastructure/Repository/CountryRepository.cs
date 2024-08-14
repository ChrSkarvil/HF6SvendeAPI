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
    public class CountryRepository : ICountryRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public CountryRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the countries.", ex);
            }
        }

        public async Task<Country?> GetCountryByNameAsync(string countryName)
        {
            try
            {
                return await _context.Countries.FirstOrDefaultAsync(l => l.Name == countryName);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the country.", ex);
            }
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            try
            {
                return await _context.Countries.FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the country.", ex);
            }
        }
    }
}
