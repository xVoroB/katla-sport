using System;

namespace KatlaSport.Services.EmployeeManagement
{
    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }

        public int ChiefEmployeeId { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public int PositionId { get; set; }

        public int EmployeeCVId { get; set; }
    }
}
