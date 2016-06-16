using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Api.Models
{
    public class Email
    {
        private bool _isActive = false;

        [Key]
        public int EmailId { get; set; }
        public User User { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Value { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (value)
                {
                    if (IsConfirmed)
                    {
                        _isActive = true;
                    }
                    else
                    {
                        //throw new UnconfirmedEmailCannotBeActiveException();
                        throw new System.Exception();
                    }
                }
                else
                {
                    _isActive = value;
                }
                
            }
        }
    }
}