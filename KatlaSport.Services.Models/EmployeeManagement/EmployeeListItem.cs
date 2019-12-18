using System;

namespace KatlaSport.Services.EmployeeManagement
{
    public class EmployeeListItem
    {
        public int Id { get; set; }

        public string ChiefEmployeeName { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public string PositionName { get; set; }

        public int EmployeeCVId { get; set; }
    }
}
