namespace KatlaSport.Services
{
    using Autofac;
    using KatlaSport.Services.Interfaces;

    /// <summary>
    /// Represents an assembly dependency registration <see cref="Module"/>.
    /// </summary>
    public class DependencyRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CatalogueManagement.CatalogueManagementService>().As<CatalogueManagement.ICatalogueManagementService>();
            builder.RegisterType<HiveManagement.HiveService>().As<HiveManagement.IHiveService>();
            builder.RegisterType<HiveAnalytics.HiveAnalysisService>().As<HiveAnalytics.IHiveAnalysisService>();
            builder.RegisterType<CustomerManagement.CustomerManagementService>().As<CustomerManagement.ICustomerManagementService>();
            builder.RegisterType<ProductManagement.ProductCategoryService>().As<ProductManagement.IProductCategoryService>();
            builder.RegisterType<ProductManagement.ProductCatalogueService>().As<ProductManagement.IProductCatalogueService>();
            builder.RegisterType<HiveManagement.HiveService>().As<HiveManagement.IHiveService>();
            builder.RegisterType<HiveManagement.HiveSectionService>().As<HiveManagement.IHiveSectionService>();
            builder.RegisterType<EmployeeManagement.EmployeeRepositoryService>().As<IRepository<EmployeeManagement.Employee>>();
            builder.RegisterType<EmployeeManagement.EmployeeCVRepositoryService>().As<IRepository<EmployeeManagement.EmployeeCV>>();
            builder.RegisterType<EmployeeManagement.PositionRepositoryService>().As<IRepository<EmployeeManagement.Position>>();
            builder.RegisterType<OrderManagement.OrderService>().As<OrderManagement.IOrderService>();
            builder.RegisterType<UserContext>().As<IUserContext>();
        }
    }
}
