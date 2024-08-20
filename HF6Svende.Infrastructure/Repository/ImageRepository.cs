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
    public class ImageRepository : IImageRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public ImageRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<Image> CreateImageAsync(Image image)
        {
            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                await _context.Entry(image).Reference(l => l.Product).LoadAsync();
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the image.", ex);
            }
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            try
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return false;
                }

                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the image.", ex);
            }
        }

        public async Task<List<Image>> GetAllImagesAsync()
        {
            try
            {
                return await _context.Images.Include(l => l.Product).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the images.", ex);
            }
        }

        public async Task<Image?> GetImageByIdAsync(int id)
        {
            try
            {
                return await _context.Images.Include(l => l.Product).FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the image.", ex);
            }
        }

        public async Task<Image> UpdateImageAsync(Image image)
        {
            try
            {
                _context.Images.Update(image);
                await _context.SaveChangesAsync();
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the image.", ex);
            }
        }
    }
}
