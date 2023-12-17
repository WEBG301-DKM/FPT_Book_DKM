using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string? Reason { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        public int Status { get; set; }
    }
}
