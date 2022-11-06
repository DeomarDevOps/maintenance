using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;

namespace MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car
{
    public interface IRegisterCarUseCase
    {
        Task<RegisterCarResult> Execute(string description, string plate);
    }
}
