using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface ICategory
    {
        List<Category> GetAll();
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);
        Category GetById(int? id);
    }
}
