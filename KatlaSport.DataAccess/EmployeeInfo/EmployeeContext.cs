namespace KatlaSport.DataAccess.EmployeeInfo
{
    internal sealed class EmployeeContext : DomainContextBase<ApplicationDbContext>, IEmployeeContext
    {
        public EmployeeContext(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEntitySet<Employee> Employees => GetDbSet<Employee>();

        public IEntitySet<EmployeeCV> EmployeeCVs => GetDbSet<EmployeeCV>();

        public IEntitySet<Position> Positions => GetDbSet<Position>();
    }
}
