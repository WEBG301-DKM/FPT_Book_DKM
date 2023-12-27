using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [Required]
        public int? BookID { get; set; }
        [ForeignKey("BookID")]
        [ValidateNever]
        public Book? Book { get; set; }
      
        public int Quantity { get; set; }
    }
}
