using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Api.Exceptions
{
    public class EmailIsAlredyTakenException: Exception
    {
        public EmailIsAlredyTakenException(string message)
            :base(message)
        {

        }
    }
}