using System;

namespace BookLibrary.ApiTest.Exceptions.PasswordExceptions
{
    class OldPasswordWrongException : Exception
    {
        public OldPasswordWrongException(string message)
            :base(message)
        {

        }
    }
}
