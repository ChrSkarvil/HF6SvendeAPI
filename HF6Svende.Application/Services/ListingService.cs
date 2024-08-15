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
using HF6SvendeAPI.Data.Entities;
using Microsoft.Extensions.Logging;

namespace HF6Svende.Application.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListingService> _logger;


        public ListingService(IListingRepository listingRepository, IProductRepository productRepository, ILogger<ListingService> logger, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _productRepository = productRepository;
            _logger = logger;
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
                _logger.LogInformation("Creating product: {@Product}", product);

                // Create the product in the repository
                var createdProduct = await _productRepository.CreateProductAsync(product);

                // Map the Listing entity from the ListingCreateDTO
                var listing = _mapper.Map<Listing>(createListingDto);
                listing.ProductId = createdProduct.Id; // Set the ProductId to the created product's ID

                // Log the listing creation
                _logger.LogInformation("Creating listing: {@Listing}", listing);

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

        public async Task<ListingDTO?> UpdateListingAsync(int id, ListingUpdateDTO updateListingDto)
        {
            try
            {
                // Get existing listing
                var listing = await _listingRepository.GetListingByIdAsync(id);
                if (listing == null) return null;

                // Mapping dto to entity
                _mapper.Map(updateListingDto, listing);

                // Update the related product fields
                listing.Product.Brand = updateListingDto.Brand;
                listing.Product.Description = updateListingDto.Description;
                listing.Product.Size = updateListingDto.Size;


                // Save the changes in the repository
                var updatedListing = await _listingRepository.UpdateListingAsync(listing);

                // Mapping back to dto
                return _mapper.Map<ListingDTO>(updatedListing);
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

    }
}
