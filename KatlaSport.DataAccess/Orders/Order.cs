using System;
using System.Collections.Generic;
using KatlaSport.DataAccess.CustomerCatalogue;
using KatlaSport.DataAccess.EmployeeInfo;

namespace KatlaSport.DataAccess.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public DateTime OrderAcceptanceDate { get; set; }

        public DateTime OrderDispatchDate { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
