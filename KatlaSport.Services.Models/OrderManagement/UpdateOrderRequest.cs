using System;
using System.Collections.Generic;
using FluentValidation.Attributes;

namespace KatlaSport.Services.OrderManagement
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime OrderAcceptanceDate { get; set; }

        public DateTime OrderDispatchDate { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
