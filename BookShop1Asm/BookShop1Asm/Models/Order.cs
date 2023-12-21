using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop1Asm.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        [Range(1, 50)]
        public int Quantity { get; set; }
        public float Total { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("BookId")]
        [ValidateNever]
        public Book Book { get; set; }

    }
}
