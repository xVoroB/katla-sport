using System;

namespace KatlaSport.Services.EmployeeManagement
{
    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }

        public Employee HeadEmployee { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public Position Position { get; set; }

        public EmployeeCV EmployeeCV { get; set; }
    }
}
