using FluentValidation;
using MaintenanceCheckinCheckout.Application.ViewModels.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;

namespace MaintenanceCheckinCheckout.Application.Validation.Car
{
    public class PickUpCarRequestValidator : AbstractValidator<PickupCarRequest>
    {
        public PickUpCarRequestValidator()
        {
            RuleFor(m => m.CarId).NotEmpty();
            RuleFor(m => m.RentedBy).NotEmpty();
        }
    }
}
