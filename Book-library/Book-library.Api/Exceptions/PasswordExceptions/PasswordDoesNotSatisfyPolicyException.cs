using System;
using System.Runtime.Serialization;

namespace BookLibrary.Api.Services
{
    [Serializable]
    internal class PasswordDoesNotSatisfyPolicyException : Exception
    {
        public PasswordDoesNotSatisfyPolicyException()
        {
        }

        public PasswordDoesNotSatisfyPolicyException(string message) : base(message)
        {
        }

        public PasswordDoesNotSatisfyPolicyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordDoesNotSatisfyPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string newPasswordValue { get; set; }
    }
}