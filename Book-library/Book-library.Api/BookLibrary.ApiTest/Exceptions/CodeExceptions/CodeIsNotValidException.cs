using System;

namespace BookLibrary.ApiTest.Exceptions.CodeExceptions
{
    class CodeIsNotValidException : Exception
    {
        public CodeIsNotValidException(string message)
            :base(message)
        {

        }
    }
}
