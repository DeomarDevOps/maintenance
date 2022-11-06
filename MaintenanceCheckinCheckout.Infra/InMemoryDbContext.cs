using MaintenanceCheckinCheckout.Domain.Models.Cars;
using System.Collections.ObjectModel;

namespace MaintenanceCheckinCheckout.Infra
{
    public class InMemoryDbContext
    {
        public Collection<Cars> Cars { get; set; }
        public Collection<PickUpCar> Pickups { get; set; }

        public InMemoryDbContext()
        {
            Cars = new Collection<Cars>();
            Pickups = new Collection<PickUpCar>();
        }
    }
}
