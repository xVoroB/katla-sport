using FluentValidation;

namespace KatlaSport.Services.EmployeeManagement
{
    public class UpdateEmployeeCVRequestValidator : AbstractValidator<UpdateEmployeeCVRequest>
    {
        public UpdateEmployeeCVRequestValidator()
        {
            RuleFor(i => i.EmployeeId).GreaterThan(1);
            RuleFor(i => i.Name).NotNull().Must(i => i.Contains(".doc") || i.Contains(".docx"));
        }
    }
}
