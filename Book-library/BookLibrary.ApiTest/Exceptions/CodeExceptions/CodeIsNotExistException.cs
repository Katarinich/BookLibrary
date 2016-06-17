using System;

namespace BookLibrary.Api.Exceptions.CodeExceptions
{
    class CodeIsNotExistException : Exception
    {
        public CodeIsNotExistException(string message)
            :base(message)
        {

        }
    }
}
