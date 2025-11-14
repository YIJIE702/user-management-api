using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Middlewares
{
    // Simple token-based middleware for demo purposes.
    // In production, use proper JWT validation via authentication schemes.
    public class TokenAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string DemoValidToken = "demo-valid-token-123";

        public TokenAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow anonymous access to swagger and health endpoints
            var path = context.Request.Path.Value ?? string.Empty;
            if (path.StartsWith("/swagger") || path.StartsWith("/health") || (context.Request.Method == "GET" && path == "/api/users")) 
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var authValues))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "Missing Authorization header." });
                return;
            }

            var auth = authValues.FirstOrDefault() ?? string.Empty;
            if (!auth.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid Authorization header." });
                return;
            }

            var token = auth.Substring("Bearer ".Length).Trim();
            if (token != DemoValidToken)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid token." });
                return;
            }

            await _next(context);
        }
    }
}
