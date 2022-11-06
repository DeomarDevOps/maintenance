using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Infra.Repositories
{
    public class CarRepository : ICarReadOnlyRepository, ICarWriteOnlyRepository
    {
        private readonly InMemoryDbContext _context;

        public CarRepository(InMemoryDbContext context)
        {
            _context = context;
        }
        public async Task Add(Cars car)
        {
            _context.Cars.Add(car);
            await Task.CompletedTask;
        }

        public async Task Delete(Cars car)
        {
            var teste = _context.Cars.FirstOrDefault(x => x.Id == car.Id);
            _context.Cars.Remove(teste);

            await Task.CompletedTask;
        }

        public async Task<Cars> GetById(Guid id)
        {
            return await Task.FromResult<Cars>(_context.Cars.SingleOrDefault(e => e.Id == id));
        }

        public Task<IList<Cars>> GetAll()
        {
            return Task.FromResult<IList<Cars>>(_context.Cars.ToList());
        }

        public async Task Update(Cars car, PickUpCar pickUp)
        {
            PickUpCar pickupEntity = PickUpCar.Load(pickUp.Id, car.Id, pickUp.RentedBy, pickUp.Latitude, pickUp.Longitude,  pickUp.TransactionDate);

            _context.Pickups.Add(pickupEntity);

            await Task.CompletedTask;

        }
    }
}
