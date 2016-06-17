using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Api
{
    class CredentialsDraft
    {
        public CredentialsDraft(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public string login { get; set; }
        public string password { get; set; }
    }
}
