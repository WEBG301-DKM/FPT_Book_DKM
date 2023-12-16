using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string? Fullname { get; set; }
        public string? Address { get; set; }

    }
}
