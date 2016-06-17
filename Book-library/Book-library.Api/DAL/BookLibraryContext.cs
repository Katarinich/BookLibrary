using BookLibrary.Api.Models;
using System.Data.Entity;

namespace BookLibrary.Api.DAL
{
    public class BookLibraryContext: DbContext
    {
        public BookLibraryContext(): base("BookLibrary")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<LoginName> LoginNames { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<ConfirmationCode> Codes { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CredentialMap());
        }
    }
}