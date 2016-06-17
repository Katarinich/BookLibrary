using System;

namespace BookLibrary.Api.Exceptions.CodeExceptions
{
    class CodeIsNotValidException : Exception
    {
        public CodeIsNotValidException(string message)
            :base(message)
        {

        }
    }
}
