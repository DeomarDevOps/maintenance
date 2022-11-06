using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;

namespace MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car
{
    public interface IGetCarsUseCase
    {
        Task<IList<CarResult>> Execute();
    }
}
