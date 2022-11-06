using FluentValidation;
using MaintenanceCheckinCheckout.Application.ViewModels.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;

namespace MaintenanceCheckinCheckout.Application.Validation.Car
{
    public class RegisterCarRequestValidator : AbstractValidator<RegisterCarRequest>
    {
        public RegisterCarRequestValidator()
        {
            RuleFor(m => m.Plate).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
