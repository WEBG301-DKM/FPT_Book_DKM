using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.Models
{
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
