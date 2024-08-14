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
                return await _context.Listings.Include(l => l.Product).Include(l => l.Customer).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the listings.", ex);
            }

        }

        public async Task<Listing?> GetListingByIdAsync(int id)
        {
            try
            {
                return await _context.Listings.Include(l => l.Product).Include(l => l.Customer).FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the listing.", ex);
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

    }
}
