using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Eodg.FirebaseAuthMiddleware
{
    /// <summary>
    /// Authorization handler for use with firebase id verification
    /// </summary>
    public class FirebaseAuthHandler : AuthorizationHandler<FirebaseAuthAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            FirebaseAuthAuthorizationRequirement requirement)
        {
            string providedToken;
            var mvcContext = context.Resource as AuthorizationFilterContext;

            try
            {
                // Get token from Auth Header
                providedToken =
                    mvcContext
                        .HttpContext
                        .Request
                        .Headers["Authorization"]
                        .ToString()
                        .Replace("Bearer ", string.Empty);

                // Verify token
                FirebaseAuth
                    .DefaultInstance
                    .VerifyIdTokenAsync(providedToken)
                    .Wait();

                context.Succeed(requirement);
            }
            catch (Exception ex)
            {
                context.Fail();

                throw new FirebaseAuthException("Unable to verify ID Token.", ex);
            }

            return Task.CompletedTask;
        }
    }
}
