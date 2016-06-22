using BookLibrary.Api.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BookLibrary.Api
{
    class UserBuilder
    {
        public User BuildUser(UserDraft userToBuild)
        {
            var user = new User();

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(double.Parse(userToBuild.dateOfBirth));
            user.DateOfBirth = dtDateTime;
            user.FirstName = userToBuild.firstName;
            user.LastName = userToBuild.lastName;
            user.UserName = userToBuild.userName;

            var address = new Address();
            address.Country = userToBuild.country;
            address.City = userToBuild.city;
            address.Zipcode = userToBuild.zipCode;
            address.State = userToBuild.state;
            address.User = user;
            user.Address = address;

            var mobilePhone = new MobilePhone();
            mobilePhone.User = user;
            mobilePhone.Value = userToBuild.mobilePhone;
            mobilePhone.IsConfirmed = false;
            user.MobilePhone = mobilePhone;

            var email = new Email();
            email.User = user;
            email.Value = userToBuild.email;
            email.IsConfirmed = false;
            email.IsActive = false;

            user.Emails.Add(email);

            var credentials = new Credentials();
            credentials.User = user;

            var password = new Password();
            string hash;

            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, userToBuild.password);
            }

            password.Value = hash;
            password.IsActive = true;
            password.ExpirationDate = DateTime.Now.AddYears(1);
            password.Credentials = credentials;

            credentials.Logins.Add(new LoginName(LoginType.Username, userToBuild.userName, credentials));
            credentials.Logins.Add(new LoginName(LoginType.Email, userToBuild.email, credentials));
            credentials.Logins.Add(new LoginName(LoginType.MobilePhone, userToBuild.mobilePhone, credentials));

            credentials.Passwords.Add(password);

            user.Credentials = credentials;

            var role = new Role();
            role.Name = "user";
            role.Users.Add(user);

            user.UserRoles.Add(role);

            return user;
        }

        string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
