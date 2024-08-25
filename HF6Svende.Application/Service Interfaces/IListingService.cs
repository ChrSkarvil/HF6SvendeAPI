using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Listing;
using HF6SvendeAPI.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IListingService
    {
        Task<List<ListingDTO>> GetAllListingsAsync();
        Task<ListingDTO?> GetListingByIdAsync(int id);
        Task<ListingDTO> CreateListingAsync(ListingCreateDTO createListingDto);
        Task<ListingDTO?> UpdateListingAsync(int id, ListingUpdateDTO updateListingDto, int? customerId, string role);
        Task<bool> DeleteListingAsync(int id);

        Task<List<ListingDTO>> GetAllVerifiedListingsAsync();
        Task<List<ListingDTO>> GetAllUnverifiedListingsAsync();

        Task<bool> SetListingVerifiedAsync(int listingId, bool verified);

    }
}
