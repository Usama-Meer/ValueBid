﻿using ValueBid.Models;

namespace ValueBid.Data.Services
{
    public interface IBidsService
    {
        Task Add(Bid bid);
        IQueryable<Bid> GetAll();

/*        Task Update(int? id, Bid updatedBid);*/

    }


}
