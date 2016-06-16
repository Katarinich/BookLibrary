using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}