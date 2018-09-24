using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Eodg.FirebaseAuthMiddleware
{
    /// <summary>
    /// Middleware for handling FirebaseAuthException in requests
    /// </summary>
    public class FirebaseAuthExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (FirebaseAuthException ex)
            {
                await HandleFirebaseAuthExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Sends a 401 on failure of firebase identity verification
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleFirebaseAuthExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return context.Response.WriteAsync(exception.AggregateExceptionMessages());
        }
    }
}
