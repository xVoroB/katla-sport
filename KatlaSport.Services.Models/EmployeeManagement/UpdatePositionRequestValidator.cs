using FluentValidation;

namespace KatlaSport.Services.EmployeeManagement
{
    public class UpdatePositionRequestValidator : AbstractValidator<UpdatePositionRequest>
    {
        public UpdatePositionRequestValidator()
        {
            RuleFor(i => i.Name).Length(2);
        }
    }
}
