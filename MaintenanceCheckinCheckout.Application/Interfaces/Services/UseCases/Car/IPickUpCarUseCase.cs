namespace MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car
{
    public interface IPickUpCarUseCase
    {
        Task<Guid> Execute(Guid carId, string rentedBy, long latitude, long longitude);
    }
}
