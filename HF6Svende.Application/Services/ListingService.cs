using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Listing;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace HF6Svende.Application.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IProductRepository _productRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;


        public ListingService(IListingRepository listingRepository, IProductRepository productRepository, IColorRepository colorRepository, 
            IImageRepository imageRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ListingDTO>> GetAllListingsAsync()
        {
            try
            {
                // Get all listings
                var listings = await _listingRepository.GetAllListingsAsync();

                // Mapping back to dto
                return _mapper.Map<List<ListingDTO>>(listings);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }

        }

        public async Task<ListingDTO?> GetListingByIdAsync(int id)
        {
            try
            {
                // Get listing by id
                var listing = await _listingRepository.GetListingByIdAsync(id);

                if (listing == null) return null;

                // Mapping back to dto
                return _mapper.Map<ListingDTO>(listing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }

        }

        public async Task<ListingDTO> CreateListingAsync(ListingCreateDTO createListingDto)
        {
            try
            {
                // Validate Product
                if (createListingDto.Product == null)
                {
                    throw new ArgumentNullException(nameof(createListingDto.Product), "Product data must be provided.");
                }

                // Map the ProductCreateDTO to Product entity
                var product = _mapper.Map<Product>(createListingDto.Product);

                product.ProductColors = await GetProductColorsAsync(createListingDto.Product.ColorNames);

                // Create the product in the repository
                var createdProduct = await _productRepository.CreateProductAsync(product);

                // Map the Listing entity from the ListingCreateDTO
                var listing = _mapper.Map<Listing>(createListingDto);
                listing.ProductId = createdProduct.Id; // Set the ProductId to the created product's ID

                // Create the listing in the repository
                var createdListing = await _listingRepository.CreateListingAsync(listing);

                // Return the mapped ListingDTO
                return _mapper.Map<ListingDTO>(createdListing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the listing.", ex);
            }
        }

        public async Task<ListingDTO?> UpdateListingAsync(int id, ListingUpdateDTO updateListingDto, int? customerId, string role)
        {
            try
            {
                // Get existing listing
                var listing = await _listingRepository.GetListingByIdAsync(id);
                if (listing == null) return null;

                // Check if the user is an admin or if they own the listing
                if (role.ToLower() != "admin" && listing.CustomerId != customerId)
                {
                    throw new UnauthorizedAccessException("User is not authorized to update the listing.");
                }

                // Mapping dto to entity
                _mapper.Map(updateListingDto, listing);

                // Update the related product fields
                listing.Product.Brand = updateListingDto.Brand;
                listing.Product.Description = updateListingDto.Description;
                listing.Product.Size = updateListingDto.Size;

                // Handle colors
                var colors = await GetProductColorsAsync(updateListingDto.ColorNames);
                listing.Product.ProductColors = colors;

                // Handle image updates
                if (updateListingDto.NewImages.Any())
                {
                    var images = updateListingDto.NewImages.Select(file => new Image
                    {
                        File = ConvertToBytes(file),
                        CreateDate = DateTime.UtcNow,
                        IsVerified = true,
                        ProductId = listing.Product.Id
                    }).ToList();

                    foreach (var image in images)
                    {
                        await _imageRepository.CreateImageAsync(image);
                    }
                }

                if (updateListingDto.ImageIdsToRemove.Any())
                {
                    foreach (var imageId in updateListingDto.ImageIdsToRemove)
                    {
                        await _imageRepository.DeleteImageAsync(imageId);
                    }
                }

                // Save the changes in the repository
                var updatedListing = await _listingRepository.UpdateListingAsync(listing);

                // Mapping back to dto
                return _mapper.Map<ListingDTO>(updatedListing);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the listing.", ex);
            }
        }

        public async Task<bool> DeleteListingAsync(int id)
        {
            try
            {
                // Get the listing to get the product ID
                var listing = await _listingRepository.GetListingByIdAsync(id);
                if (listing == null)
                {
                    throw new Exception("Listing not found.");
                }

                // Delete the listing
                var success = await _listingRepository.DeleteListingAsync(id);

                // Delete the product
                var productDeleted = await _productRepository.DeleteProductAsync(listing.ProductId);
                if (!productDeleted)
                {
                    throw new Exception("Failed to delete the product.");
                }

                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the listing and its product.", ex);
            }
        }

        private async Task<List<ProductColor>> GetProductColorsAsync(List<string> colorNames)
        {
            var productColors = new List<ProductColor>();

            foreach (var colorName in colorNames)
            {
                var color = await _colorRepository.GetColorByNameAsync(colorName);

                if (color != null)
                {
                    productColors.Add(new ProductColor
                    {
                        ColorId = color.Id
                    });
                }
            }

            return productColors;
        }

        public async Task<List<ListingDTO>> GetAllVerifiedListingsAsync()
        {
            try
            {
                // Get all verified listings
                var listings = await _listingRepository.GetAllVerifiedListingsAsync();

                // Mapping back to dto
                return _mapper.Map<List<ListingDTO>>(listings);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }
        }

        public async Task<List<ListingDTO>> GetAllUnverifiedListingsAsync()
        {
            try
            {
                // Get all unverified listings
                var listings = await _listingRepository.GetAllUnverifiedListingsAsync();

                // Mapping back to dto
                return _mapper.Map<List<ListingDTO>>(listings);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }
        }

        public async Task<bool> SetListingVerifiedAsync(int listingId, bool verified)
        {
            try
            {
                await _listingRepository.SetListingVerifiedAsync(listingId, verified);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the listing.", ex);
            }
            
        }

        public async Task<int> GetUnverifiedListingCountAsync()
        {
            try
            {
                return await _listingRepository.GetUnverifiedListingCountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the unverified listing count.", ex);
            }
        }

        public async Task<int> GetListingCountAsync()
        {
            try
            {
                return await _listingRepository.GetListingCountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing count.", ex);
            }
        }

        private byte[] ConvertToBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
