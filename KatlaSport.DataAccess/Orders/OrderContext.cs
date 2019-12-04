namespace KatlaSport.DataAccess.Orders
{
    internal sealed class OrderContext : DomainContextBase<ApplicationDbContext>, IOrderContext
    {
        public OrderContext(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEntitySet<Order> Orders => GetDbSet<Order>();

        public IEntitySet<OrderProduct> OrderProducts => GetDbSet<OrderProduct>();
    }
}