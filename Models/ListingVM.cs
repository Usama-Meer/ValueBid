using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValueBid.Models
{
    public class ListingVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public bool IsSold { get; set; }=false;
        
        //added for Image file
        public IFormFile Image { get; set; }

        [Required]
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]

        public IdentityUser User { get; set; }

        public List<Bid> Bids { get; set; }

        public List<Comment> Comments { get; set; } 


    }
}
