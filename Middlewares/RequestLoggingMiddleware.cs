namespace Apotek.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine($"[REQUEST] [{timestamp}] {method} {path}");

            await _next(context);
        }
    }
}
