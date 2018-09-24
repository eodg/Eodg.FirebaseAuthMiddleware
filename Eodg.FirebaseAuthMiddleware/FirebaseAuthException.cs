using System;
using System.Runtime.Serialization;

namespace Eodg.FirebaseAuthMiddleware
{
    public class FirebaseAuthException : Exception
    {
        public FirebaseAuthException()
            : base()
        {

        }

        public FirebaseAuthException(string message)
            : base(message)
        {

        }

        public FirebaseAuthException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public FirebaseAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
