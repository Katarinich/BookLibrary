using BookLibrary.Api.DAL;
using System.Web.Mvc;

namespace BookLibrary.Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            //var context = new BookLibraryContext();
            //var credentialsDraft = new CredentialsDraft("i.ivanov", "123456");
            //Login.Authentification(credentialsDraft);
            //var userDraft = new UserDraft();
            //var userBuilder = new UserBuilder();
            //var user = userBuilder.BuildUser(userDraft);

            //Registration.SaveUser(user);
            //var user = new User();
            //user.DateOfBirth = DateTime.Parse("1921-07-10");
            //user.FirstName = "Ivan2";
            //user.LastName = "Ivanov";

            //var address = new Address();
            //address.Country = "Belarus";
            //address.City = "Grodno";
            //address.ZipCode = "123446";
            //address.State = "qwerert";
            //address.User = user;
            //user.Address = address;

            //var mobilePhone = new MobilePhone();
            //mobilePhone.User = user;
            //mobilePhone.Value = "1246576";
            //mobilePhone.IsConfirmed = true;
            //user.MobilePhone = mobilePhone;

            //var credentials = new Credentials();
            //credentials.User = user;

            //var loginName = new LoginName();
            //loginName.Type = LoginType.Username;
            //loginName.Value = "124354564";
            //loginName.Credentials = credentials;

            //var password = new Password();
            //password.Value = "123456";
            //password.IsActive = true;
            //password.ExpirationDate = DateTime.Parse("1921-07-10");
            //password.Credentials = credentials;

            //credentials.Logins.Add(loginName);
            //credentials.Passwords.Add(password);

            //user.Credentials = credentials;

            //var role = new Role();
            //role.Name = "admin";
            //role.Users.Add(user);

            //user.UserRoles.Add(role);

            var context = new BookLibraryContext();
            context.Database.CreateIfNotExists();
            ////context.Users.Add(user);
            ////context.SaveChanges();
            //var query = context.Users.Include("Address").Include("Credentials").Include("MobilePhone").Include("UserRoles");
            //var data = query.ToList();

            return "";
        }
    }
}
