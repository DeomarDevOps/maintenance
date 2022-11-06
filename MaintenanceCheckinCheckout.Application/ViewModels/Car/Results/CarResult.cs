using MaintenanceCheckinCheckout.Domain.Models.Cars;
namespace MaintenanceCheckinCheckout.Application.ViewModels.Car.Results
{
    public sealed class CarResult
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Plate { get; set; }

        public bool IsRented { get; }

        public CarResult() { }
        public CarResult(Cars car)
        {
            Id = car.Id;
            Description = car.Description;
            Plate = car.Plate;
            IsRented = car.IsRented;

        }
    }
}
