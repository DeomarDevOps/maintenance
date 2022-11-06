using AutoMapper;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public class GetCarsUseCase : IGetCarsUseCase
    {
        private readonly ICarReadOnlyRepository _carReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetCarsUseCase(ICarReadOnlyRepository carReadOnlyRepository,
            IMapper mapper)
        {
            _carReadOnlyRepository = carReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<IList<CarResult>> Execute()
        {
            var cars = await _carReadOnlyRepository.GetAll();

            var result = _mapper.Map<List<CarResult>>(cars);

            return result;
        }
    }
}
