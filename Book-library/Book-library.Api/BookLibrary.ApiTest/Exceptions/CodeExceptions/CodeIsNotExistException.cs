using System;

namespace BookLibrary.ApiTest.Exceptions.CodeExceptions
{
    class CodeIsNotExistException : Exception
    {
        public CodeIsNotExistException(string message)
            :base(message)
        {

        }
    }
}
