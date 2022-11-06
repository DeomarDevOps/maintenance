using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public class DeleteCarUseCase : IDeleteCarUseCase
    {
        private readonly ICarWriteOnlyRepository _carWriteOnlyRepository;
        private readonly ICarReadOnlyRepository _carReadOnlyRepository;

        public DeleteCarUseCase(ICarWriteOnlyRepository carWriteOnlyRepository,
            ICarReadOnlyRepository carReadOnlyRepository)
        {
            _carWriteOnlyRepository = carWriteOnlyRepository;
            _carReadOnlyRepository = carReadOnlyRepository;
        }
        public async Task Execute(Guid id)
        {
            var car = await _carReadOnlyRepository.GetById(id);
            if (car == null)
            {
                throw new CarNotFoundException($"O carro {id} não existe");
            }

            await _carWriteOnlyRepository.Delete(car);
        }
    }
}
