using System;

namespace BookLibrary.ApiTest.Services
{
    class EmailNotFoundException : Exception
    {
        public EmailNotFoundException(string message) : base(message)
        {
        }
    }
}