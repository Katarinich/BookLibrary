using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLibrary.Api.Models;

namespace BookLibrary.Api
{
    [TestClass]
    public class UserBuilderTest
    {
        [TestMethod]
        public void BookLibraryUserBuiderResultTest()
        {
            var userDraft = new UserDraft();
            var userBuilder = new UserBuilder();
            var user = userBuilder.BuildUser(userDraft); 

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(double.Parse(userDraft.dateOfBirth));

            Assert.AreEqual(user.DateOfBirth, dtDateTime);
            Assert.AreEqual(user.FirstName, userDraft.firstName);
            Assert.AreEqual(user.LastName, userDraft.lastName);
            Assert.AreEqual(user.UserName, userDraft.userName);

            Assert.IsNotNull(user.Address);
            Assert.AreEqual(user.Address.Country, userDraft.country);
            Assert.AreEqual(user.Address.City, userDraft.city);
            Assert.AreEqual(user.Address.State, userDraft.state);
            Assert.AreEqual(user.Address.Zipcode, userDraft.zipCode);
            Assert.AreSame(user.Address.User, user);

            Assert.IsNotNull(user.MobilePhone);
            Assert.AreEqual(user.MobilePhone.Value, userDraft.mobilePhone);
            Assert.AreEqual(user.MobilePhone.IsConfirmed, false);
            Assert.AreSame(user.MobilePhone.User, user);

            Assert.IsNotNull(user.Emails[0]);
            Assert.AreEqual(user.Emails[0].Value, userDraft.email);
            Assert.AreEqual(user.Emails[0].IsConfirmed, false);
            Assert.AreSame(user.Emails[0].User, user);

            Assert.IsNotNull(user.Credentials);
            Assert.IsNotNull(user.Credentials.Logins);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.Email).Value, userDraft.email);
            Assert.AreSame(user.Credentials.Logins.Find(x => x.Type == LoginType.Email).Credentials, user.Credentials);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.MobilePhone).Value, userDraft.mobilePhone);
            Assert.AreSame(user.Credentials.Logins.Find(x => x.Type == LoginType.MobilePhone).Credentials, user.Credentials);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.Username).Value, userDraft.userName);
            Assert.AreSame(user.Credentials.Logins.Find(x => x.Type == LoginType.Username).Credentials, user.Credentials);
            Assert.IsNotNull(user.Credentials.Passwords);
            Assert.AreEqual(user.Credentials.Passwords.Count, 1);
            Assert.AreEqual(user.Credentials.Passwords[0].Value, userDraft.password);
            Assert.AreEqual(user.Credentials.Passwords[0].IsActive, true);
            Assert.IsNotNull(user.Credentials.Passwords[0].ExpirationDate);
            Assert.AreSame(user.Credentials.Passwords[0].Credentials, user.Credentials);
            Assert.AreEqual(user.Credentials.User, user);

            Assert.IsNotNull(user.UserRoles);
            Assert.AreEqual(user.UserRoles.Count, 1);
            Assert.AreEqual(user.UserRoles[0].Name, "user");
        }

       
    }
}
