using System.Collections.Generic;

namespace KatlaSport.Services.OrderManagement
{
    public class OrderEntityConverter
    {
        public UpdateOrderRequest SingleRequestConverter(UpdateOrdersRequest createRequest)
        {
            List<UpdateOrderProductRequest> product = new List<UpdateOrderProductRequest>()
            {
                new UpdateOrderProductRequest() {ProductId = createRequest.OrderProductsId}
            };
            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                CustomerId = createRequest.CustomerId,
                EmployeeId = createRequest.EmployeeId,
                OrderAcceptanceDate = createRequest.OrderAcceptanceDate,
                OrderDispatchDate = createRequest.OrderDispatchDate,
                OrderProducts = product
            };
            return request;
        }
    }
}
