using System;

namespace BookLibrary.Api.Models
{
    public class Token
    {
        public DateTime ExpirationDate { get; set; }
        public string Value { get; set; }
    }
}