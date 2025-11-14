using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace UserManagementAPI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var request = context.Request;
            _logger.LogInformation("Incoming request {method} {path}", request.Method, request.Path);

            // Copy original response body to read status code after
            var originalBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var statusCode = context.Response.StatusCode;
            sw.Stop();
            _logger.LogInformation("Outgoing response {statusCode} for {method} {path} took {ms}ms", statusCode, request.Method, request.Path, sw.ElapsedMilliseconds);

            // copy back
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await context.Response.Body.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    }
}
