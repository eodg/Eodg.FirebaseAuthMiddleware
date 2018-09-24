using Microsoft.AspNetCore.Authorization;

namespace Eodg.FirebaseAuthMiddleware
{
    /// <summary>
    /// See: <see cref="https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.1#why-would-i-want-multiple-handlers-for-a-requirement" />
    /// </summary>
    public class FirebaseAuthAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
