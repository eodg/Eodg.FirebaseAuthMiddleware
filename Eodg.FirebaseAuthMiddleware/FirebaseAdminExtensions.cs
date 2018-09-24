using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Eodg.FirebaseAuthMiddleware
{
    /// <summary>
    /// Extension methods to enable functionality of FirebaseAuthMiddleware
    /// </summary>
    public static class FirebaseAdminExtensions
    {
        /// <summary>
        /// Default policy name
        /// </summary>
        public const string POLICY_NAME = "Eodg.FirebaseAuthMiddleware.Policy";

        #region Extension Methods

        /// <summary>
        /// Adds firebase authentication to the service collection using file path for the key
        /// </summary>
        /// <param name="services">IServiceCollection instance to which you will be adding the auth policy</param>
        /// <param name="firebaseAdminKeyPath">File path to the firebase key</param>
        public static void AddFirebaseAdminFromFile(
            this IServiceCollection services, 
            string firebaseAdminKeyPath)
        {
            AddAuthPolicy(services);

            CreateApp(GoogleCredential.FromFile(firebaseAdminKeyPath));
        }

        /// <summary>
        /// Adds firebase authentication to the service collection using an access token for the key
        /// </summary>
        /// <param name="services">IServiceCollection instance to which you will be adding the auth policy</param>
        /// <param name="accessToken">The access token to use for the credential</param>
        /// <param name="accessMethod">
        /// Optional. The Google.Apis.Auth.OAuth2.IAccessMethod to use within 
        /// this credential. If null, will default to Google.Apis.Auth.OAuth2.BearerToken.AuthorizationHeaderAccessMethod.
        /// </param>
        public static void AddFirebaseAdminFromAccessToken(
            this IServiceCollection services, 
            string accessToken, 
            IAccessMethod accessMethod = null)
        {
            AddAuthPolicy(services);

            CreateApp(GoogleCredential.FromAccessToken(accessToken, accessMethod));
        }

        /// <summary>
        /// Adds firebase authentication to the service collection using a compute credential for the key (not recommended)
        /// </summary>
        /// <param name="services">IServiceCollection instance to which you will be adding the auth policy</param>
        /// <param name="computeCredential">The compute credential to use in the returned Google.Apis.Auth.OAuth2.GoogleCredential</param>
        public static void AddFirebaseAdminFromComputeCredential(
            this IServiceCollection services,
            ComputeCredential computeCredential = null)
        {
            AddAuthPolicy(services);

            CreateApp(GoogleCredential.FromComputeCredential(computeCredential));
        }

        /// <summary>
        /// Adds firebase authentication to the service collection using a JSON string for the key
        /// </summary>
        /// <param name="services">IServiceCollection instance to which you will be adding the auth policy</param>
        /// <param name="json">JSON string of key</param>
        public static void AddFirebaseAdminFromJson(
            this IServiceCollection services,
            string json)
        {
            AddAuthPolicy(services);

            CreateApp(GoogleCredential.FromJson(json));
        }

        /// <summary>
        /// Adds firebase authentication to the service collection using a stream for the JSON string for the key
        /// </summary>
        /// <param name="services">IServiceCollection instance to which you will be adding the auth policy</param>
        /// <param name="stream">Stream containing the JSON key</param>
        public static void AddFirebaseAdminFromStream(
            this IServiceCollection services,
            Stream stream)
        {
            AddAuthPolicy(services);

            CreateApp(GoogleCredential.FromStream(stream));
        }

        /// <summary>
        /// Extension method to add the FirebaseAuthExceptionMiddleware to your app.
        /// </summary>
        /// <param name="app">IApplicationBuilder instance to which you will be adding the middleware</param>
        public static void UseFirebaseAuthExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FirebaseAuthExceptionMiddleware>();
        }

        /// <summary>
        /// Extension method for use in this library to concat all inner exception 
        /// messages to the main one.
        /// </summary>
        /// <param name="ex">Exception to aggregate</param>
        /// <returns>The exception's message, plus messages of all inner exceptions</returns>
        internal static string AggregateExceptionMessages(this Exception ex)
        {
            var message = ex.Message;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                message += $"\nInner Exception: \n{ex.Message}";
            }

            return message;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Adds an authorization policy for using Firebase Ttoken verification to the provided service collection
        /// </summary>
        /// <param name="services">Service collection to which the auth policy will be added</param>
        /// <param name="policyName">(Optional) A policy name</param>
        private static void AddAuthPolicy(IServiceCollection services, string policyName = POLICY_NAME)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(policyName, policy =>
                {
                    policy.Requirements.Add(new FirebaseAuthAuthorizationRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, FirebaseAuthHandler>();
        }

        /// <summary>
        /// Creates the global Firebase App
        /// </summary>
        /// <param name="googleCredential">Valid google credential to use to create the app.</param>
        private static void CreateApp(GoogleCredential googleCredential)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = googleCredential
            });
        }

        #endregion
    }
}
