using MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public sealed  class RegisterCarUseCase : IRegisterCarUseCase
    {
        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;

        public RegisterCarUseCase(ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            this.carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<RegisterCarResult> Execute(string description, string plate)
        {
            Cars car = new Cars(description, plate);

            await carWriteOnlyRepository.Add(car);

            RegisterCarResult result = new RegisterCarResult(car);

            return result;
        }
    }
}
