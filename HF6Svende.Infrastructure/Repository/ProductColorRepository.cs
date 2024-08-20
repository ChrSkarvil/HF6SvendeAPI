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
    public class ProductColorRepository : IProductColorRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public ProductColorRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<ProductColor> CreateProductColorAsync(ProductColor productColor)
        {
            try
            {
                _context.ProductColors.Add(productColor);
                await _context.SaveChangesAsync();
                return productColor;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the product color.", ex);
            }
        }

        public async Task<bool> DeleteProductColorAsync(int id)
        {
            try
            {
                var productColor = await _context.ProductColors.FindAsync(id);
                if (productColor == null)
                {
                    return false;
                }

                _context.ProductColors.Remove(productColor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product color.", ex);
            }
        }

        public async Task<List<ProductColor>> GetAllProductColorsAsync()
        {
            try
            {
                return await _context.ProductColors.Include(l => l.Product).Include(l => l.Color).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product colors.", ex);
            }
        }

        public async Task<ProductColor?> GetProductColorByIdAsync(int id)
        {
            try
            {
                return await _context.ProductColors.Include(l => l.Product).Include(l => l.Color).FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product color.", ex);
            }
        }
        public async Task<ProductColor?> GetProductColorByNameAsync(string name)
        {
            try
            {
                return await _context.ProductColors.Include(l => l.Product).Include(l => l.Color).FirstOrDefaultAsync(l => l.Color.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product color.", ex);
            }
        }
    }
}
