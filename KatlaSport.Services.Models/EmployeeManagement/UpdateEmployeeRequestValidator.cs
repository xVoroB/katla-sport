using FluentValidation;

namespace KatlaSport.Services.EmployeeManagement
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(i => i.Name).Length(3, 30);
            RuleFor(i => i.Position).NotNull();
        }
    }
}
