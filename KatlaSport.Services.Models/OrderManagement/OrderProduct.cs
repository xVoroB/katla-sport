using KatlaSport.Services.ProductManagement;

namespace KatlaSport.Services.OrderManagement
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public Product Product { get; set; }
    }
}