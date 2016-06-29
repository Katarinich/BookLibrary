using System;

namespace BookLibrary.Api.Exceptions
{
    public class LoginsAreNotUniqException: Exception
    {
        public LoginsAreNotUniqException(string message)
            : base(message)
        {

        }
    }
}