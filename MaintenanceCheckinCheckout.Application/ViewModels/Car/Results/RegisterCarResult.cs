using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.ViewModels.Car.Results
{
    public class RegisterCarResult
    {
        public CarResult Car { get; }

        public RegisterCarResult(Cars car)
        {
            Car = new CarResult(car);

        }
    }
}
