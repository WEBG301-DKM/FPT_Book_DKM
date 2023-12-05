using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Alias { get; set; }
        public string? Photo { get; set; }
        public string Info { get; set; }
    }
}
