using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllImagesAsync();
        Task<Image?> GetImageByIdAsync(int id);
        Task<Image> CreateImageAsync(Image image);
        Task<Image> UpdateImageAsync(Image image);
        Task<bool> DeleteImageAsync(int id);
    }
}
