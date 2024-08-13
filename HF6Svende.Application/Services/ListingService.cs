using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;


        public ListingService(IListingRepository listingRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _mapper = mapper;
        }

        public async Task<List<ListingDto>> GetAllListingsAsync()
        {
            try
            {
                var listings = await _listingRepository.GetAllListingsAsync();
                return _mapper.Map<List<ListingDto>>(listings);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the listing.", ex);
            }

        }

        public async Task<ListingDto?> GetListingByIdAsync(int id)
        {
            try
            {
                var listing = await _listingRepository.GetListingByIdAsync(id);
                if (listing == null) return null;
                return _mapper.Map<ListingDto>(listing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the listing.", ex);
            }

        }

        public async Task<ListingDto> CreateListingAsync(CreateListingDto createListingDto)
        {
            try
            {
                // Mapping dto to entity
                var listing = _mapper.Map<Listing>(createListingDto);

                // Create listing in repository
                var createdListing = await _listingRepository.CreateListingAsync(listing);

                // Mapping back to dto
                return _mapper.Map<ListingDto>(createdListing);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the listing.", ex);
            }
        }

    }
}
