﻿using BookShop1Asm.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookShop1Asm.ViewModels.BookViewModel
{
    public class CreateUpdateVM
    {
        public Book Book { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MyCategories { get; set; }
        [Display(Name ="Categories")]
        public int[] CatIDs { get; set; }
    }
}