namespace MaintenanceCheckinCheckout.Application.Services.Exceptions
{
    internal sealed class CarNotFoundException : ServiceException
    {
        internal CarNotFoundException(string message)
            : base(message)
        { }
    }
}
