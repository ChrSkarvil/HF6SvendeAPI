using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Listing;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IListingService
    {
        Task<List<ListingDto>> GetAllListingsAsync();
        Task<ListingDto?> GetListingByIdAsync(int id);
        Task<ListingDto> CreateListingAsync(CreateListingDto createListingDto);
        Task<ListingDto> UpdateListingAsync(int id, UpdateListingDto updateListingDto);
        Task<bool> DeleteListingAsync(int id);

    }
}
