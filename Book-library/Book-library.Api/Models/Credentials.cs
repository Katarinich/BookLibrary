using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class Credentials
    {
        public Credentials()
        {
            Logins = new List<LoginName>();
            Passwords = new List<Password>();
        }
        [Key]
        public int CredentialId { get; set; }
        public User User { get; set; }
        public List<LoginName> Logins { get; set; }
        public List<Password> Passwords { get; set; }
    }
}