using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Core.Interfaces;
using HF6SvendeAPI.Data;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HF6Svende.Infrastructure.Repository
{
    public class ListingRepository : IListingRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public ListingRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Listing>> GetAllListingsAsync()
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listings.", ex);
            }

        }

        public async Task<Listing?> GetListingByIdAsync(int id)
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }
        }

        public async Task<List<Listing>> GetListingsByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .Where(l => l.CustomerId == customerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listing.", ex);
            }
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            try
            {
                _context.Listings.Add(listing);
                await _context.SaveChangesAsync();
                await _context.Entry(listing).Reference(l => l.Customer).LoadAsync();
                return listing;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the listing.", ex);
            }

        }

        public async Task<Listing> UpdateListingAsync(Listing listing)
        {
            try
            {
                _context.Listings.Update(listing);

                //Update product
                _context.Entry(listing.Product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return listing;
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
                var listing = await _context.Listings.FindAsync(id);
                if (listing == null)
                {
                    return false;
                }

                _context.Listings.Remove(listing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the listing.", ex);
            }
        }

        public async Task SetSoldDateAsync(int listingId, DateTime soldDate)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null)
            {
                throw new KeyNotFoundException("Listing not found");
            }

            listing.SoldDate = soldDate;
            _context.Listings.Update(listing);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Listing>> GetAllVerifiedListingsAsync()
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .Where(l => l.IsListingVerified == true && l.IsActive == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listings.", ex);
            }
        }

        public async Task<List<Listing>> GetAllUnverifiedListingsAsync()
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .Where(l => l.IsListingVerified == false)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the listings.", ex);
            }
        }

        public async Task SetListingVerifiedAsync(int listingId, bool verified, DateTime? denyDate)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null)
            {
                throw new KeyNotFoundException("Listing not found");
            }

            listing.IsListingVerified = verified;
            listing.DenyDate = denyDate;
            _context.Listings.Update(listing);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUnverifiedListingCountAsync()
        {
            try
            {
                return await _context.Listings
                    .CountAsync(l => l.IsListingVerified == false && l.DenyDate == null);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting verified listings.", ex);
            }
        }

        public async Task<int> GetDeniedListingCountAsync()
        {
            try
            {
                return await _context.Listings
                    .CountAsync(l => l.DenyDate != null);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting verified listings.", ex);
            }
        }

        public async Task<int> GetListingCountAsync()
        {
            try
            {
                return await _context.Listings
                    .CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting listings.", ex);
            }
        }

        public async Task<List<Listing>> GetAllDeniedListingsAsync()
        {
            try
            {
                return await _context.Listings
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Category)
                            .ThenInclude(c => c.Gender)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.Images)
                    .Include(l => l.Product)
                        .ThenInclude(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                    .Include(l => l.Customer)
                    .Where(l => l.DenyDate != null)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the denied listings.", ex);
            }
        }
    }
}
