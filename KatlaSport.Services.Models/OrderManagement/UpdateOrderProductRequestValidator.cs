using FluentValidation;

namespace KatlaSport.Services.OrderManagement
{
    public class UpdateOrderProductRequestValidator : AbstractValidator<UpdateOrderProductRequest>
    {
        public UpdateOrderProductRequestValidator()
        {
            RuleFor(op => op.Product).NotNull();
        }
    }
}
