# Eodg.FirebaseAuthMiddleware

This is a simple piece of middleware you can utilize to authorize http requests via a Firebase ID Token.

## Usage

1. Install the package `Eodg.FirebaseAuthMiddleware` into your web project.

1. In the `ConfigureServices` method of the `Startup` class add one (and only one) of the following method signatures:

   * `services.AddFirebaseAdminFromFile(string firebaseAdminKeyPath)`
   * `services.AddFirebaseAdminFromAccessToken(string accessToken, IAccessMethod accessMethod = null)`
   * `services.AddFirebaseAdminFromStream(Stream stream)`
   * `services.AddFirebaseAdminFromComputeCredential(ComputeCredential computeCredential = null)`
   * `services.AddFirebaseAdminFromJson(string json)`

1. In the `Configure` method of the `Startup` class, add the following at the very beginning of the method:

    * `app.UseFirebaseAuthExceptionMiddleware();`

1. In any controller/method (depending if you want the scope to be controller wide or only for a method) add the following attribute:

    * `[Authorize(FirebaseAdminUtil.POLICY_NAME)]`

1. When making a request, add the following header to the request:

    * `"Authorization": "Bearer {token}"` where `{token}` is the Firebase ID token to verify

[Sample API](./Eodg.FirebaseAuthMiddleware.SampleApi)
[Sample UI](./Eodg.FirebaseAuthMiddleware.SampleUI)