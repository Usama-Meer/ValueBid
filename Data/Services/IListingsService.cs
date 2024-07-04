using ValueBid.Models;

namespace ValueBid.Data.Services
{
    public interface IListingsService
    {
        public IQueryable<Listing> GetAll();
    }
}
