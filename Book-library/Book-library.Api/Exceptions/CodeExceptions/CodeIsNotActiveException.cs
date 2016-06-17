using System;

namespace BookLibrary.Api.Exceptions.CodeExceptions
{
    class CodeIsNotActiveException :Exception
    {
        public CodeIsNotActiveException(string message)
            : base(message)
        {
        }
    }
}
