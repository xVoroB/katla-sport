using System.Collections.Generic;

namespace KatlaSport.DataAccess.EmployeeInfo
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
