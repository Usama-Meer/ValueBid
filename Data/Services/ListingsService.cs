using Microsoft.EntityFrameworkCore;
using ValueBid.Models;

namespace ValueBid.Data.Services
{
    public class ListingsService : IListingsService
    {
        public readonly ApplicationDbContext _context;

        public IQueryable<Listing> GetAll()
        {

            //added from index route of controller
            var applicationDbContext=_context.Listings.Include(I=>I.User);
            return applicationDbContext;
        }

        //implementation of Add method
        public async Task Add(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
        }

        public async Task<Listing> GetById(int? id)
        {
            //copied from ListingsController and added Comments,Bids, Bids users
            var listing = await _context.Listings
                        .Include(l => l.User)
                        .Include(l => l.Comments)
                        .Include(l => l.Bids)
                        .ThenInclude(l=>l.User)
                        .FirstOrDefaultAsync(m => m.Id == id);
            
            return listing;
        }

        public ListingsService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
