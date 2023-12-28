using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop1Asm.Models
{
    public class OrderBook
    {
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]
        public Order Order { get; set; }
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public float BookPrice { get; set; }
        public int Quantity { get; set; }

    }
}
