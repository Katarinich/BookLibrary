using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_library.Api.Exceptions
{
    public class LoginsAreNotUniqException: Exception
    {
        public LoginsAreNotUniqException(string message)
            : base(message)
        {

        }
    }
}