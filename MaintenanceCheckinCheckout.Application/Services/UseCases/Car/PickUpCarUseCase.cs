using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public sealed class PickUpCarUseCase : IPickUpCarUseCase
    {

        private readonly ICarReadOnlyRepository _carReadOnlyRepository;
        private readonly ICarWriteOnlyRepository _carWriteOnlyRepository;

        public PickUpCarUseCase(ICarReadOnlyRepository carReadOnlyRepository, ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            _carReadOnlyRepository = carReadOnlyRepository;
            _carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<Guid> Execute(Guid carId, string rentedBy, long latitude, long longitude)
        {
            try
            {
                Cars car = await _carReadOnlyRepository.GetById(carId);
                if (car == null)
                    throw new CarNotFoundException($"O carro {carId} não existe");

                var pickUp = car.Pickup(rentedBy, latitude, longitude);

                await this._carWriteOnlyRepository.Update(car, pickUp);

                return pickUp.Id;
            }
            catch (CarCannotBePickupExcepction ex)
            {
                throw new ServiceException(ex.Message);
            }            
        }
    }
}
