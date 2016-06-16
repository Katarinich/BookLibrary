using System;

namespace BookLibrary.ApiTest.Exceptions.CodeExceptions
{
    class CodeExpirationDateIsUpException : Exception
    {
        public CodeExpirationDateIsUpException(string message)
            :base(message)
        {

        }
    }
}
