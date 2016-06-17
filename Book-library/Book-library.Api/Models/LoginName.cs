using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class LoginName
    {
        public LoginName(LoginType type, string value, Credentials credentials)
        {
            Type = type;
            Value = value;
            Credentials = credentials;
        }

        public LoginName()
        {

        }
        [Key]
        public int LoginId { get; set; }
        public LoginType Type { get; set; }
        public string Value { get; set; }
        public Credentials Credentials { get; set; }
    }
}