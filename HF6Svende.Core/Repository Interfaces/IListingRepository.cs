using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Interfaces
{
    public interface IListingRepository
    {
        Task<List<Listing>> GetAllListingsAsync();
        Task<Listing?> GetListingByIdAsync(int id);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<Listing> UpdateListingAsync(Listing listing);
        Task<bool> DeleteListingAsync(int id);

        Task SetSoldDateAsync(int listingId, DateTime soldDate);



    }
}
