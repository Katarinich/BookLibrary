using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class Token
    {
        [Key]
        public int TokenId { get; set; } 
        public DateTime ExpirationDate { get; set; }
        public string Value { get; set; }
        public bool isActive { get; set; }
    }
}