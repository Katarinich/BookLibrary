using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class ConfirmationCode
    {
        [Key]
        public int CodeId { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public Email Email { get; set; }
        public ConfirmationCodeType Type {get; set;}
    }
}