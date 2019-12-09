using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.Orders;
using DbOrder = KatlaSport.DataAccess.Orders.Order;

namespace KatlaSport.Services.OrderManagement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderContext _context;
        private readonly IUserContext _userContext;

        public OrderService(IOrderContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<List<OrderListItem>> GetOrdersAsync()
        {
            var dbOrders = await _context.Orders.OrderBy(s => s.Id).ToArrayAsync();
            var orders = dbOrders.Select(o => Mapper.Map<OrderListItem>(o)).ToList();
            return orders;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var dbOrders = await _context.Orders.Where(h => h.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbOrder, OrderListItem>(dbOrders[0]);
        }

        public async Task<Order> AddOrderAsync(UpdateOrderRequest createRequest)
        {
            var dbOrders = await _context.Orders.Where(h => h.Id == createRequest.Id).ToArrayAsync();
            if (dbOrders.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var dbOrder = Mapper.Map<UpdateOrderRequest, DbOrder>(createRequest);
            dbOrder.CustomerId = _userContext.UserId;
            dbOrder.EmployeeId = 1;
            _context.Orders.Add(dbOrder);

            await _context.SaveChangesAsync();

            return Mapper.Map<Order>(dbOrder);
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var dbOrders = await _context.Orders.Where(p => p.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbOrder = dbOrders[0];

            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> UpdateOrderAsync(int orderId, UpdateOrderRequest updateRequest)
        {
            var dbOrders = await _context.Orders.Where(h => h.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbOrder = dbOrders[0];
            Mapper.Map(updateRequest, dbOrder);

            await _context.SaveChangesAsync();
            return Mapper.Map<Order>(dbOrder);
        }
    }
}
