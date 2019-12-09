using System;
using System.Collections.Generic;

namespace KatlaSport.Services.OrderManagement
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime OrderAcceptanceDate { get; set; }

        public DateTime OrderDispatchDate { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
