namespace MaintenanceCheckinCheckout.Domain.Models.Cars
{
    public sealed class Cars : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Description { get; set; }
        public string Plate { get; set; }
        public bool IsRented { get; set; }

        public Cars(string description, string plate)
        {
            Id = Guid.NewGuid();
            Description = description;
            Plate = plate;
        }

        public PickUpCar Pickup(string rentedBy, long latitude, long longitude)
        {
            if (this.IsRented)
                throw new CarCannotBePickupExcepction($"O carro {Id} já está alugado!");


            var pickUp = new PickUpCar(Id, rentedBy, latitude, longitude);

            this.IsRented = true;

            return pickUp;
        }

        private Cars() { }

        public static Cars Load(Guid id, string description, string Plate, bool isRented)
        {
            Cars car = new Cars();
            car.Id = id;
            car.Description = description;
            car.Plate = Plate;
            car.IsRented = isRented;
            return car;

        }
    }
}
