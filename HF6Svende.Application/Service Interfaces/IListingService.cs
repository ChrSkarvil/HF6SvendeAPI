using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IListingService
    {
        Task<List<ListingDto>> GetAllListingsAsync();
        Task<ListingDto?> GetListingByIdAsync(int id);
        Task<ListingDto> CreateListingAsync(CreateListingDto createListingDto);
    }
}
