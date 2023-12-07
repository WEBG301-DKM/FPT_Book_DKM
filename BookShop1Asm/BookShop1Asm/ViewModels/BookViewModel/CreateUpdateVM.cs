using BookShop1Asm.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop1Asm.ViewModels.BookViewModel
{
    public class CreateUpdateVM
    {
        public Book Book { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MyCategories { get; set; }
        public string[] SelectedCategories { get; set; }
    }
}
