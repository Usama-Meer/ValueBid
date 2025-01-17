﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValueBid.Models;

namespace ValueBid.Data.Services
{
    public interface IListingsService
    {
        public IQueryable<Listing> GetAll();
        
        //added add method


        Task Add(Listing listing);

        //added for details
        Task<Listing> GetById(int? id);

        Task SaveChanges();

        Task UpdateListing(Listing listing);

        Task<IEnumerable<IdentityUser>> GetAllUsers();
    }
}
