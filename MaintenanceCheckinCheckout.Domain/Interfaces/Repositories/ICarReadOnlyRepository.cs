using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Domain.Interfaces.Repositories
{

    public interface ICarReadOnlyRepository
    {
        Task<Cars> GetById(Guid id);
        Task<IList<Cars>> GetAll();
    }
}
