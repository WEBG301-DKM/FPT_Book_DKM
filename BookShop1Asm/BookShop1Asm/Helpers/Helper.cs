using AutoMapper;
using BookShop1Asm.Models;
using BookShop1Asm.ViewModels.CategoryViewModel;

namespace BookShop1Asm.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
