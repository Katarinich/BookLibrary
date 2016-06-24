using System;

namespace BookLibrary.Api.Exceptions.CodeExceptions
{
    public class CodeHasWrongTypeException : Exception
    {
        public CodeHasWrongTypeException(string message)
            :base(message)
        {

        }
    }
}