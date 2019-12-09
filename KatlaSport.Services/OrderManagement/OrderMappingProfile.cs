using AutoMapper;
using DataAccessOrder = KatlaSport.DataAccess.Orders.Order;
using DataAccessOrderProduct = KatlaSport.DataAccess.Orders.OrderProduct;

namespace KatlaSport.Services.OrderManagement
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<DataAccessOrder, Order>();
            CreateMap<DataAccessOrder, OrderListItem>()
                .ForMember(r => r.CustomerName, opt => opt.MapFrom(p => p.Customer.Name))
                .ForMember(r => r.EmployeeName, opt => opt.MapFrom(p => p.Employee.Name))
                .ForMember(r => r.EmployeePosition, opt => opt.MapFrom(p => p.Employee.Position));
            CreateMap<DataAccessOrderProduct, OrderProduct>();

            CreateMap<UpdateOrderRequest, DataAccessOrder>();
            CreateMap<UpdateOrderProductRequest, DataAccessOrderProduct>();
        }
    }
}
