namespace MaintenanceCheckinCheckout.Application.Services.Exceptions
{
    public class ServiceException : Exception
    {
        internal ServiceException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
