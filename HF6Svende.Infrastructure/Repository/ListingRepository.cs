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
            return await _context.Listings.Include(l => l.Product).Include(l => l.Customer).ToListAsync();

        }

        public async Task<Listing?> GetListingByIdAsync(int id)
        {
            return await _context.Listings.Include(l => l.Product).Include(l => l.Customer).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
            await _context.Entry(listing).Reference(l => l.Customer).LoadAsync();
            return listing;
        }

    }
}
