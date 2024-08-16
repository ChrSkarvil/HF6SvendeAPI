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
    public class ColorRepository : IColorRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public ColorRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<List<Color>> GetAllColorsAsync()
        {
            try
            {
                return await _context.Colors.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the colors.", ex);
            }
        }

        public async Task<Color?> GetColorByIdAsync(int id)
        {
            try
            {
                return await _context.Colors.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the color.", ex);
            }
        }

        public async Task<Color?> GetColorByNameAsync(string color)
        {
            try
            {
                return await _context.Colors.FirstOrDefaultAsync(l => l.Name == color);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the color.", ex);
            }
        }
    }
}
