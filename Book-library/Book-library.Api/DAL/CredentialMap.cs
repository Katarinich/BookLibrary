using BookLibrary.Api.Models;
using System.Data.Entity.ModelConfiguration;

namespace BookLibrary.Api.DAL
{
    public class CredentialMap : EntityTypeConfiguration<Credentials>
    {
        public CredentialMap()
        {
            HasMany(c => c.Logins)
                .WithRequired(l => l.Credentials);
            HasMany(c => c.Passwords)
                .WithRequired(p => p.Credentials);
        }
    }
}