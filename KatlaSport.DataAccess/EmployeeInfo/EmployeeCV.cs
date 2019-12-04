using System;
using System.Collections.Generic;

namespace KatlaSport.DataAccess.EmployeeInfo
{
    public class EmployeeCV
    {
        public int Id { get; set; }

        public byte[] File { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
