using System;

namespace BookLibrary.Api.Exceptions.CodeExceptions
{
    class CodeExpirationDateIsUpException : Exception
    {
        public CodeExpirationDateIsUpException(string message)
            :base(message)
        {

        }
    }
}
