using Apotek.Helpers;
using System.Net;
using System.Text.Json;

namespace Apotek.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ResponseAPI
                {
                    Status = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Data = null
                };

                var result = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
