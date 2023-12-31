﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever]
        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        [ValidateNever]
        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        //public List<OrderBook> OrderDetail { get; set; }
        //public List<CartBook> CartBooks { get; set; }

    }
}
