using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_library.Api.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string TokenValue { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
    }
}