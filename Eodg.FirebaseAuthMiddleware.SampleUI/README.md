# Eodg.FirebaseAuthMiddleware.SampleUI

This is a sample implementation of a web api utilizing the FirebaseAuthMiddleware.

## Setup

1. Modify the `wwwroot/firebase-config.js` to reflect you key per the [firebase admin documentation](https://firebase.google.com/docs/web/setup#add_firebase_to_your_app).

   *NOTE:* Do *NOT* use this outside of development! *NEVER* use private key references or info for any purpose other than local development!

2. Ensure the `VERIFICATION_SERVER_URL` `const` declaration in `wwwroot/index.js` accurately reflects your API server.

## Notes

*  Currently this sample only has the capability to sign in with a Google account.

*  Your host machine might have to have a software clock reset to work if you encounter an issue with future dates.
