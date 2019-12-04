namespace KatlaSport.DataAccess.Orders
{
    public interface IOrderContext
    {
        IEntitySet<Order> Orders { get; }

        IEntitySet<OrderProduct> OrderProducts { get; }
    }
}
