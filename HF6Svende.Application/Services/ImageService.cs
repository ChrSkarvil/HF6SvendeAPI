using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Image;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;


        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        public async Task<ImageDTO> CreateImageAsync(ImageCreateDTO createImageDto)
        {
            try
            {
                // Validate the file
                if (createImageDto.File == null || createImageDto.File.Length == 0)
                {
                    throw new ArgumentException("File cannot be null");
                }

                // Map to Image entity
                var imageEntity = _mapper.Map<Image>(createImageDto);

                // Save the image to the repository
                await _imageRepository.CreateImageAsync(imageEntity);

                // Return the mapped ImageDTO
                return _mapper.Map<ImageDTO>(imageEntity);
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
                // Delete image
                var success = await _imageRepository.DeleteImageAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the image.", ex);
            }
        }

        public async Task<List<ImageDTO>> GetAllImagesAsync()
        {
            try
            {
                // Get all images
                var images = await _imageRepository.GetAllImagesAsync();

                // Mapping back to dto
                return _mapper.Map<List<ImageDTO>>(images);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the image.", ex);
            }
        }

        public async Task<ImageDTO?> GetImageByIdAsync(int id)
        {
            try
            {
                // Get image by id
                var image = await _imageRepository.GetImageByIdAsync(id);

                if (image == null) return null;

                // Mapping back to dto
                return _mapper.Map<ImageDTO>(image);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the image.", ex);
            }
        }

        public async Task<ImageDTO?> UpdateImageAsync(int id, ImageUpdateDTO updateImageDto)
        {
            try
            {
                // Get existing image
                var image = await _imageRepository.GetImageByIdAsync(id);
                if (image == null) return null;

                // Mapping dto to entity
                _mapper.Map(updateImageDto, image);

                // Save the changes in the repository
                var updatedImage = await _imageRepository.UpdateImageAsync(image);

                // Mapping back to dto
                return _mapper.Map<ImageDTO>(updatedImage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the image.", ex);
            }
        }
    }
}
