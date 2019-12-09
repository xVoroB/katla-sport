namespace KatlaSport.DataAccess.Orders
{
    public interface IOrderContext : IAsyncEntityStorage
    {
        IEntitySet<Order> Orders { get; }

        IEntitySet<OrderProduct> OrderProducts { get; }
    }
}
