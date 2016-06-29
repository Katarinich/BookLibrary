using System;

namespace BookLibrary.Api.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string Message)
            : base(Message)
        {

        }
    }
}