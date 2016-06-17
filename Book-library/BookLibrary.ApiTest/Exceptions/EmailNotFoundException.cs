using System;

namespace BookLibrary.Api.Services
{
    class EmailNotFoundException : Exception
    {
        public EmailNotFoundException(string message) : base(message)
        {
        }
    }
}