using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }
        public string Value { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public Credentials Credentials { get; set; }
    }
}