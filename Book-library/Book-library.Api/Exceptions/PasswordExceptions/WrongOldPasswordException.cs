using System;

namespace BookLibrary.Api.Exceptions.PasswordExceptions
{
    class OldPasswordWrongException : Exception
    {
        public OldPasswordWrongException(string message)
            :base(message)
        {

        }
    }
}
