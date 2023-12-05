using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Cover { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

    }
}
