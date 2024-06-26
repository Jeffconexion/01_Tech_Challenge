using System.Diagnostics;

namespace LocalFriendzApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _logger.LogInformation("Handling request: {Method} {Url}", context.Request.Method, context.Request.Path);

                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation("Finished handling request. Time taken: {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "An error occurred while processing the request. Time taken: {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
