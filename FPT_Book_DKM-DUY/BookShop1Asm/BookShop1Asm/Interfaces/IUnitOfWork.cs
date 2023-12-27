namespace BookShop1Asm.Interfaces
{
    public interface IUnitOfWork
    {
        IBook Book { get; }
        ICategory Category { get; }
        IAuthor Author { get; }
        IRequest Request { get; }
        ICart Cart { get; }
        IOrder Order { get; }
        IOrderBook OrderBook { get; }
        void Save();
        void AddRange(IEnumerable<Object> objects);
    }
}
