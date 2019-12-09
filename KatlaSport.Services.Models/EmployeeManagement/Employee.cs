using System;

namespace KatlaSport.Services.EmployeeManagement
{
    public class Employee
    {
        public int Id { get; set; }

        public Employee ChiefEmployee { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public Position Position { get; set; }

        public EmployeeCV EmployeeCV { get; set; }
    }
}
