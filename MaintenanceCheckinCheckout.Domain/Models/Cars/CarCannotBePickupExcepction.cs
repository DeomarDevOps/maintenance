namespace MaintenanceCheckinCheckout.Domain.Models.Cars
{
   
    public sealed class CarCannotBePickupExcepction : Exception
    {
        internal CarCannotBePickupExcepction(string message)
            : base(message)
        { }
    }
}
