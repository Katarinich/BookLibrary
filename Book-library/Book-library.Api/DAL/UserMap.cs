using BookLibrary.Api.Models;
using System.Data.Entity.ModelConfiguration;

namespace BookLibrary.Api.DAL
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(u => u.UserId);
            HasRequired(u => u.Credentials)
                .WithRequiredPrincipal(c => c.User);
            Property(u => u.FirstName);
            Property(u => u.LastName);
            Property(u => u.DateOfBirth);
            HasMany(u => u.Emails)
                .WithRequired(e => e.User);
            Property(u => u.UserName);
            HasRequired(u => u.MobilePhone)
                .WithRequiredPrincipal(mp => mp.User);
            HasRequired(u => u.Address)
                .WithRequiredPrincipal(a => a.User);
            HasMany(u => u.UserRoles)
                .WithMany(r => r.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("RoleRefId");
                    cs.ToTable("UserRole");
                });
        }
    }
}