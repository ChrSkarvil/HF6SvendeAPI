using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Image;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IImageService
    {
        Task<List<ImageDTO>> GetAllImagesAsync();
        Task<ImageDTO?> GetImageByIdAsync(int id);
        Task<ImageDTO> CreateImageAsync(ImageCreateDTO createImageDto);
        Task<ImageDTO?> UpdateImageAsync(int id, ImageUpdateDTO updateImageDto);
        Task<bool> DeleteImageAsync(int id);
    }
}
