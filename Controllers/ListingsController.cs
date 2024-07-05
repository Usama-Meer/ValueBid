using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValueBid.Data;
using ValueBid.Data.Services;
using ValueBid.Models;

namespace ValueBid.Controllers
{
    public class ListingsController : Controller
    {
        //importing IlistingsService
        private readonly IListingsService _listingsService;

        //added IwebHostEnvironment
        private readonly IWebHostEnvironment _webHostEnvironment;

        //added IWebHostEnviornment in the constructor
        public ListingsController(IListingsService listingsService, IWebHostEnvironment webHostEnvironment)
        {
            _listingsService = listingsService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _listingsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //calling listingsService's GetById method
            var listing= await _listingsService.GetById(id);
                        
                    
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }
        
                // GET: Listings/Create

                //updated Create route
                public IActionResult Create()
                {

                    return View();
                }

                // POST: Listings/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create(ListingVM listing)
                {
                    //checks whether there is image or not
                    if (listing.Image!=null)
                    {
                        //sets an upload directory
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    
                        //sets fileName
                        string fileName=listing.Image.FileName;
                        
                        //sets file path
                        string filePath=Path.Combine(uploadDir,fileName);

                        //create file
                        using(var fileStream=new FileStream(filePath,FileMode.Create))
                        {
                            listing.Image.CopyTo(fileStream);

                        }

                        //object is created
                        var listObj=new Listing 
                        {
                            Title=listing.Title,
                            Description=listing.Description,
                            IdentityUserId=listing.IdentityUserId,
                            ImagePath=fileName,
                        };

                        await _listingsService.Add(listObj);

                        //redirect to the index
                        return RedirectToAction(nameof(Index));
                    }
                    //in case of error is adding listing, it will return to listing page
                    return View(listing);
                    
                }
        /*
                // GET: Listings/Edit/5
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var listing = await _context.Listings.FindAsync(id);
                    if (listing == null)
                    {
                        return NotFound();
                    }
                    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
                    return View(listing);
                }

                // POST: Listings/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,IsSold,ImagePath,IdentityUserId")] Listing listing)
                {
                    if (id != listing.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(listing);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ListingExists(listing.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
                    return View(listing);
                }
        
                // GET: Listings/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var listing = await _context.Listings
                        .Include(l => l.User)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (listing == null)
                    {
                        return NotFound();
                    }

                    return View(listing);
                }
        /*
                // POST: Listings/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var listing = await _context.Listings.FindAsync(id);
                    if (listing != null)
                    {
                        _context.Listings.Remove(listing);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool ListingExists(int id)
                {
                    return _context.Listings.Any(e => e.Id == id);
                }

        */
    }
}