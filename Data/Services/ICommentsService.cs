using ValueBid.Models;

namespace ValueBid.Data.Services
{
    public interface ICommentsService
    {
        Task Add(Comment comment);
    }
}
