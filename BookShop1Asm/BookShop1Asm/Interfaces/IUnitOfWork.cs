namespace BookShop1Asm.Interfaces
{
    public interface IUnitOfWork
    {
        IBook Book { get; }
        ICategory Category { get; }
        void Save();
    }
}
