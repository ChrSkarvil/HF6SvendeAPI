﻿using System;
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

        Task<List<Listing>> GetAllVerifiedListingsAsync();
        Task<List<Listing>> GetAllUnverifiedListingsAsync();
        Task<List<Listing>> GetAllDeniedListingsAsync();

        Task<List<Listing>> GetListingsByCustomerIdAsync(int customerId);

        Task<int> GetUnverifiedListingCountAsync();
        Task<int> GetDeniedListingCountAsync();
        Task<int> GetListingCountAsync();

        Task SetSoldDateAsync(int listingId, DateTime soldDate);
        Task SetListingVerifiedAsync(int listingId, bool verified, DateTime? denyDate);
        Task SetListingDeleteDateAsync(int listingId, bool deleted, DateTime? deleteDate);




    }
}
