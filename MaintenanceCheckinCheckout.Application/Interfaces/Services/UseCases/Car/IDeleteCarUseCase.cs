namespace MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car
{
    public interface IDeleteCarUseCase
    {
        Task Execute(Guid id);
    }
}
