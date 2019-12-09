using FluentValidation;

namespace KatlaSport.Services.OrderManagement
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(o => o.CustomerId).GreaterThan(0);
            RuleFor(o => o.EmployeeId).GreaterThan(0);
        }
    }
}