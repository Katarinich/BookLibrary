using System;

namespace BookLibrary.Api
{
    public class UserDraft
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string dateOfBirth { get; set; }
        public string mobilePhone { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string addressLine { get; set; }
        public string zipCode { get; set; } 
    }
}
