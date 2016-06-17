using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class Address
    {
        [Key]
        public int AdressId { get; set; }
        public string Country { get; set; }
        public User User { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        //public List<string> AddressLines { get; set; }  
        public string ZipCode { get; set; }    
    }
}