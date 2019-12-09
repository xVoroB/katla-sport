using System;

namespace KatlaSport.Services.EmployeeManagement
{
    public class EmployeeCV
    {
        public int Id { get; set; }

        public byte[] File { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
