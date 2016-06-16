using System;

namespace BookLibrary.ApiTest.Exceptions.CodeExceptions
{
    class CodeIsNotActiveException :Exception
    {
        public CodeIsNotActiveException(string message)
            : base(message)
        {
        }
    }
}
