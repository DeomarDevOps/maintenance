using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Domain.Models;

namespace MaintenanceCheckinCheckout.API.Helpers
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate next;
        ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(ILogger<ApiExceptionMiddleware> logger, RequestDelegate next)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ApiExceptionMiddleware> logger)
        {
            var message = exception.Message;

            if (exception is ServiceException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                logger.LogInformation(exception.Message, exception);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.LogError(exception.Message, exception);
                message = "Não foi possível processar sua requisição, tente mais tarde";
            }

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}
