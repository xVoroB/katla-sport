using System;
using System.Collections.Generic;
using KatlaSport.DataAccess.Orders;

namespace KatlaSport.DataAccess.EmployeeInfo
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int PositionId { get; set; }

        public virtual Position Position { get; set; }

        public int? ChiefEmployeeId { get; set; }

        public virtual Employee ChiefEmployee { get; set; }

        public int EmployeeCVId { get; set; }

        public virtual EmployeeCV EmployeeCV { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
