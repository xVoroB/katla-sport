using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.Orders
{
    internal sealed class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("orders");
            HasKey(i => i.Id);
            HasRequired(i => i.Customer).WithMany(i => i.Orders).HasForeignKey(i => i.CustomerId);
            HasRequired(i => i.Employee).WithMany(i => i.Orders).HasForeignKey(i => i.EmployeeId);
            Property(i => i.Id).HasColumnName("order_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.CustomerId).HasColumnName("customer_id").IsRequired();
            Property(i => i.EmployeeId).HasColumnName("employee_id").IsRequired();
            Property(i => i.OrderAcceptanceDate).HasColumnName("order_acceptance_date").IsRequired();
            Property(i => i.OrderDispatchDate).HasColumnName("order_dispatch_date").IsRequired();
        }
    }
}