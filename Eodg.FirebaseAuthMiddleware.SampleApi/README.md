# Eodg.FirebaseAuthMiddleware.SampleApi

This is a sample implementation of a web api utilizing the FirebaseAuthMiddleware.

## Setup

This sample assumes you have your firebase key stored locally as a json file. 
You will need to modify `FirebaseAdminKeyPath` value in the `appsettings.json` file to reflect the location of your key.

## Endpoints

### Health Check Controller

Used to ensure API server is online

Route: `api/healthcheck`
Accepts: `GET`
Returns: `200` status code

### Test Firebase Auth Controller

Used to validate a token

Route: `api/testfirebaseauth`
Accepts: `GET`
Header: `"Authorization": "Bearer {token}"` where `{token}` is the firebase token to verify
Returns: `200` status code if successful, `401` status code if the token could not be verified, `500` status code for any other error.
