using KatlaSport.DataAccess.ProductStore;

namespace KatlaSport.DataAccess.Orders
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int StoredItemId { get; set; }

        public virtual StoreItem StoreItem { get; set; }
    }
}
