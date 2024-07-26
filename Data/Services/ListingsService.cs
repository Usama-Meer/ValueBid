using ValueBid.Models;
using Microsoft.EntityFrameworkCore;
using ValueBid.Data.Services;
using ValueBid.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace ValueBid.Data.Services
{
    public class ListingsService : IListingsService
    {
        private readonly ApplicationDbContext _context;


        public ListingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Listing listing)
        {

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Listing> GetAll()
        {
            var applicationDbContext = _context.Listings.Include(l => l.User).AsNoTracking();
            return applicationDbContext;
        }

        public async Task<Listing> GetById(int? id)
        {
            var listing = await _context.Listings.AsNoTracking()
                .Include(l => l.User)
                .Include(l => l.Comments)
                .ThenInclude(l => l.User)
                .Include(l => l.Bids)
                .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            return listing;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateListing(Listing listing)
        {
            _context.Listings.Update(listing);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }
        }
    } 

