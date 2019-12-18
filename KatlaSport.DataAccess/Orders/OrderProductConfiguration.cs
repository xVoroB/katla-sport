using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.Orders
{
    internal sealed class OrderProductConfiguration : EntityTypeConfiguration<OrderProduct>
    {
        public OrderProductConfiguration()
        {
            ToTable("order_products");
            HasKey(i => i.Id);
            HasRequired(i => i.Order).WithMany(i => i.OrderProducts).HasForeignKey(i => i.OrderId);
            HasRequired(i => i.StoreItem).WithMany(i => i.OrderProducts).HasForeignKey(i => i.StoredItemId);
            Property(i => i.Id).HasColumnName("order_products_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.OrderId).HasColumnName("order_id").IsRequired();
            Property(i => i.StoredItemId).HasColumnName("stored_item_id").IsRequired();
        }
    }
}