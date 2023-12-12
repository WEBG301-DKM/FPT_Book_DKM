namespace BookShop1Asm.Interfaces
{
    public interface IUnitOfWork
    {
        IBook Book { get; }
        ICategory Category { get; }
        IAuthor Author { get; }
        void Save();
        void AddRange(IEnumerable<Object> objects);
    }
}
