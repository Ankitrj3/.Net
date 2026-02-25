using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middleware
{
    //Step-1
    public sealed class QueryBlockMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly string BLOCKED_TOKEN = "d" + "o" + "g";

        public QueryBlockMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var q = context.Request.Query["q"].ToString();

            if (!string.IsNullOrWhiteSpace(q) &&
                q.Contains(BLOCKED_TOKEN, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Invalid query: blocked token detected.");
                return;
            }

            await _next(context);

            if (!context.Response.HasStarted)
            {
                context.Response.Headers["X-Middleware-Executed"] = "QueryBlockMiddleware";
            }
        }
    }
}
