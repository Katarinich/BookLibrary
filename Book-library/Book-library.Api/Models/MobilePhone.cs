using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Api.Models
{
    public class MobilePhone
    {
        [Key]
        public int MobilePhoneId { get; set; }
        public User User { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Value { get; set; }
        public bool IsConfirmed { get; set; }
    }
}