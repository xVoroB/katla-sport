namespace KatlaSport.Services.EmployeeManagement
{
    public class EmployeeEntityConverter
    {
        public EmployeeListItem EmployeeToListItem(Employee employee)
        {
            return new EmployeeListItem()
            {
                Id = employee.Id,
                Name = employee.Name,
                Location = employee.Location,
                EmployeeCVId = employee.EmployeeCV.Id,
                ChiefEmployeeName = (employee.ChiefEmployee != null) ? employee.ChiefEmployee.Name : "Employee doesn't have a chief",
                PositionName = employee.Position.Name
            };
        }

        public Employee EmployeeRequestToEntity(UpdateEmployeeRequest entity)
        {
            return new Employee()
            {
                Id = entity.Id,
                Position = new Position() { Id = entity.PositionId },
                EmployeeCV = new EmployeeCV() { Id = entity.EmployeeCVId },
                Name = entity.Name,
                Location = entity.Location,
                ChiefEmployee = entity.ChiefEmployeeId == 0 ? null : new Employee() { Id = entity.ChiefEmployeeId }
            };
        }
    }
}
