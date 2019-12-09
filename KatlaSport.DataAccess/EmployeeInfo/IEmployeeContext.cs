namespace KatlaSport.DataAccess.EmployeeInfo
{
    public interface IEmployeeContext : IAsyncEntityStorage
    {
        IEntitySet<Employee> Employees { get; }

        IEntitySet<EmployeeCV> EmployeeCVs { get; }

        IEntitySet<Position> Positions { get; }
    }
}
