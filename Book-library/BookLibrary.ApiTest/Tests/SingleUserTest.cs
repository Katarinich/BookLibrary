using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using System.Linq;
using BookLibrary.Api.Managers;
using BookLibrary.Api.Services;
using BookLibrary.Api.Exceptions.CodeExceptions;
using BookLibrary.Api.Exceptions.PasswordExceptions;
using SimpleInjector;
using Moq;
using BookLibrary.Api.Services.NotificationTransport;

namespace BookLibrary.Api.Tests
{
    [TestClass]
    public class SingleUserTest
    {
        private BookLibraryContext _context;
        private IUserManager _userManager;
        private IEmailManager _emailManager;
        private IConfirmationCodeManager _confirmationCodeManager;
        private Container _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new Container();

            _container.Register<BookLibraryContext, BookLibraryContext>(Lifestyle.Singleton);
            _container.Register<IUserManager, UserManager>(Lifestyle.Singleton);
            _container.Register<IConfirmationCodeManager, ConfirmationCodeManager>(Lifestyle.Singleton);
            _container.Register<IEmailManager, EmailManager>(Lifestyle.Singleton);
            _container.Register<IPasswordPolicy, PasswordPolicy>();
            _container.Register<ICodeValidationRule, CodeValidationRule>();
            _container.Register<IConfirmationCodeService, ConfirmationCodeService>();
            _container.Register<IEmailConfirmationService, EmailConfirmationService>();
            _container.Register<IConfirmationSenderService, ConfirmationSenderService>();
            _container.Register<IPasswordHasher, PasswordHasher>();
            _container.Register<ICodeGenerator, CodeGenerator>();
            _container.Register<INotificationTransportService, NotificationTransportService>();

            _context = _container.GetInstance<BookLibraryContext>();
            var userBuilder = new UserBuilder();

            _userManager = _container.GetInstance<UserManager>();

            _confirmationCodeManager = _container.GetInstance<ConfirmationCodeManager>();

            _emailManager = _container.GetInstance<EmailManager>();

            var confirmationSenderService = _container.GetInstance<ConfirmationSenderService>();

            var randomUserDraftGenerator = new RandomUserDraftGenerator();
            var userDraft = randomUserDraftGenerator.GenerateUserDraft();

            var user = userBuilder.BuildUser(userDraft);
            user.Emails[0].IsConfirmed = true;
            user.ChangeEmailTo(userDraft.email);

            var registrationService = _container.GetInstance<RegistrationService>();

            registrationService.RegisterUser(user);

            confirmationSenderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailConfirmation);

            confirmationSenderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailConfirmation);

            _context.Codes.ToList()[1].IsActive = false;

            confirmationSenderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailConfirmation);

            _context.Codes.ToList()[2].ExpirationDate = new DateTimeOffset(1971, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.Delete();
        }

        [TestMethod]
        public void AddUserTest()
        {
            var userBuilder = new UserBuilder();

            var randomUserDraftGenerator = new RandomUserDraftGenerator();
            var userDraft = randomUserDraftGenerator.GenerateUserDraft();

            var user = userBuilder.BuildUser(userDraft);

            var registrationService = _container.GetInstance<RegistrationService>();

            registrationService.RegisterUser(user);

            var addedUser = _userManager.GetUserByLogin(userDraft.userName);

            Assert.AreEqual(user.DateOfBirth.ToString(), addedUser.DateOfBirth.ToString());
            Assert.AreEqual(user.FirstName, addedUser.FirstName);
            Assert.AreEqual(user.LastName, addedUser.LastName);
            Assert.AreEqual(user.UserName, addedUser.UserName);

            Assert.AreEqual(user.Address.Country, addedUser.Address.Country);
            Assert.AreEqual(user.Address.City, addedUser.Address.City);
            Assert.AreEqual(user.Address.State, addedUser.Address.State);
            Assert.AreEqual(user.Address.ZipCode, addedUser.Address.ZipCode);
            Assert.AreSame(addedUser.Address.User, addedUser);

            Assert.IsNotNull(addedUser.MobilePhone);
            Assert.AreEqual(user.MobilePhone.Value, addedUser.MobilePhone.Value);
            Assert.AreEqual(user.MobilePhone.IsConfirmed, addedUser.MobilePhone.IsConfirmed);
            Assert.AreSame(addedUser.MobilePhone.User, addedUser);

            Assert.IsNotNull(addedUser.Emails[0]);
            Assert.AreEqual(user.Emails[0].Value, addedUser.Emails[0].Value);
            Assert.AreEqual(user.Emails[0].IsConfirmed, addedUser.Emails[0].IsConfirmed);
            Assert.AreSame(addedUser.Emails[0].User, addedUser);

            Assert.IsNotNull(addedUser.Credentials);
            Assert.IsNotNull(addedUser.Credentials.Logins);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.Email).Value, addedUser.Emails[0].Value);
            Assert.AreSame(addedUser.Credentials.Logins.Find(x => x.Type == LoginType.Email).Credentials, addedUser.Credentials);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.MobilePhone).Value, addedUser.MobilePhone.Value);
            Assert.AreSame(addedUser.Credentials.Logins.Find(x => x.Type == LoginType.MobilePhone).Credentials, addedUser.Credentials);
            Assert.AreEqual(user.Credentials.Logins.Find(x => x.Type == LoginType.Username).Value, addedUser.UserName);
            Assert.AreSame(addedUser.Credentials.Logins.Find(x => x.Type == LoginType.Username).Credentials, addedUser.Credentials);
            Assert.IsNotNull(addedUser.Credentials.Passwords);
            Assert.AreEqual(addedUser.Credentials.Passwords.Count, 1);
            Assert.AreEqual(user.Credentials.Passwords[0].Value, addedUser.Credentials.Passwords[0].Value);
            Assert.AreEqual(user.Credentials.Passwords[0].IsActive, addedUser.Credentials.Passwords[0].IsActive);
            Assert.IsNotNull(addedUser.Credentials.Passwords[0].ExpirationDate);
            Assert.AreSame(addedUser.Credentials.Passwords[0].Credentials, addedUser.Credentials);
            Assert.AreEqual(addedUser.Credentials.User, addedUser);

            Assert.IsNotNull(addedUser.UserRoles);
            Assert.AreEqual(addedUser.UserRoles.Count, 1);
            Assert.AreEqual(user.UserRoles[0].Name, addedUser.UserRoles[0].Name);
        }

        [TestMethod]
        public void LoginPositiveTest()
        {
            var user = _userManager.GetUserById(1);
            var passwordValue = "123456";

            Assert.IsNotNull(AuthentificationTest(user.UserName, passwordValue));
        }

        [TestMethod]
        public void LoginFullNegativeTest()
        {
            Assert.IsNull(AuthentificationTest("", ""));
        }

        [TestMethod]
        public void LoginPasswordNegativeTest()
        {
            var user = _userManager.GetUserById(1);

            Assert.IsNull(AuthentificationTest(user.UserName, ""));
        }

        [TestMethod]
        public void AuthentificationLoginNegativeTest()
        {
            Assert.IsNull(AuthentificationTest("", "123456"));
        }

        public User AuthentificationTest(string userName, string password)
        {
            var loginService = _container.GetInstance<AuthentificationService>();
            var credentialsDraft = new CredentialsDraft(userName, password);
            return loginService.Authentificate(credentialsDraft);
        }

        [TestMethod]
        public void SendConfirmationPositiveTest()
        {
            var mock = new Mock<INotificationTransportService>();

            var codeGenrator = new CodeGenerator();

            //var confirmationSenderService = _container.GetInstance<ConfirmationSenderService>();
            var confirmationSenderService = new ConfirmationSenderService(_confirmationCodeManager, _emailManager, codeGenrator, mock.Object);

            var emailValue = _context.Emails.ToList()[0].Value;
            confirmationSenderService.SendConfirmation(emailValue, ConfirmationCodeType.EmailConfirmation);
            mock.Verify(foo => foo.SendNotification("", ""), Times.AtLeastOnce());
        }

        [TestMethod]
        [ExpectedException(typeof(EmailNotFoundException))]
        public void SendConfirmationNegativeTest()
        {
            var confirmationSenderService = _container.GetInstance<ConfirmationSenderService>();

            var emailValue = "wredfds";
            confirmationSenderService.SendConfirmation(emailValue, ConfirmationCodeType.EmailConfirmation);
        }

        [TestMethod]
        public void RecoverPasswordPositiveTest()
        {
            var passwordRecoveryService = _container.GetInstance<PasswordRecoveryService>();
            var passwordHasher = _container.GetInstance<PasswordHasher>();

            var newPasswordValue = "123456";

            passwordRecoveryService.RecoverPassword(newPasswordValue, _context.Codes.ToList()[0].Value);

            Assert.AreEqual(passwordHasher.GetHash(newPasswordValue), _context.Passwords.ToList()[1].Value);
            Assert.IsFalse(_context.Passwords.ToList()[0].IsActive);
            Assert.AreEqual(_context.Users.ToList()[0].UserId, _context.Passwords.ToList()[1].Credentials.User.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(CodeIsNotValidException))]
        public void RecoverPasswordNegativeNotActiveCodeTest()
        {
            RecoverPasswordTest(_context.Codes.ToList()[1].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(CodeIsNotValidException))]
        public void RecoverPasswordNegativeNotExistCodeTest()
        {
            RecoverPasswordTest("");
        }

        [TestMethod]
        [ExpectedException(typeof(CodeIsNotValidException))]
        public void RecoverPasswordNegativeExpirationDateTest()
        {
            RecoverPasswordTest(_context.Codes.ToList()[2].Value);
        }

        public void RecoverPasswordTest(string codeValue)
        {
            var passwordRecoveryService = _container.GetInstance<PasswordRecoveryService>();

            var newPasswordValue = "123456";

            passwordRecoveryService.RecoverPassword(newPasswordValue, codeValue);
        }

        [TestMethod]
        public void ChangePasswordPositiveTest()
        {
            var passwordChangeService = _container.GetInstance<PasswordChangeService>();
            var passwordHasher = _container.GetInstance<PasswordHasher>();
            var oldPassword = _context.Passwords.ToList()[0];
            var newPasswordValue = "1234567";

            passwordChangeService.ChangePassword(_context.Users.ToList()[0].UserId, oldPassword.Value, newPasswordValue);

            Assert.AreEqual(passwordHasher.GetHash(newPasswordValue), _context.Passwords.ToList()[1].Value);
            Assert.IsFalse(_context.Passwords.ToList()[0].IsActive);
            Assert.AreEqual(_context.Users.ToList()[0].UserId, _context.Passwords.ToList()[1].Credentials.User.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(NewPasswordDoesNotApplyPolicyException))]
        public void ChangePasswordNegativeIsNotUniqTest()
        {
            ChangePasswordNegativeTest(_context.Passwords.ToList()[0].Value, _context.Passwords.ToList()[0].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(OldPasswordWrongException))]
        public void ChangePasswordNegativeWrongOldPasswordTest()
        {
            ChangePasswordNegativeTest("", "123456");
        }

        public void ChangePasswordNegativeTest(string oldPasswordValue, string newPasswordValue)
        {
            var passwordChangeService = _container.GetInstance<PasswordChangeService>();

            passwordChangeService.ChangePassword(_context.Users.ToList()[0].UserId, oldPasswordValue, newPasswordValue);
        }

        [TestMethod]
        public void SendToNewEmailTest()
        {
            var emailChangeService = _container.GetInstance<EmailChangeService>();

            emailChangeService.InitiateChangeEmailProcess(1, "a@gmail.com");
            emailChangeService.SendConfirmationToNewEmail(_context.Codes.ToList()[3].Value);

            Assert.AreEqual(_context.Emails.ToList()[1].Value, "a@gmail.com");
            Assert.IsFalse(_context.Emails.ToList()[1].IsActive);
            Assert.IsFalse(_context.Emails.ToList()[1].IsConfirmed);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            var emailChangeService = _container.GetInstance<EmailChangeService>();

            emailChangeService.InitiateChangeEmailProcess(1, "a@gmail.com");

            emailChangeService.SendConfirmationToNewEmail(_context.Codes.ToList()[3].Value);

            emailChangeService.ChangeEmail( _context.Codes.ToList()[4].Value);

            _context.SaveChanges();

            Assert.IsTrue(_context.Emails.ToList()[1].IsActive);
            Assert.IsTrue(_context.Emails.ToList()[1].IsConfirmed);
            Assert.IsFalse(_context.Emails.ToList()[0].IsActive);
            Assert.AreEqual(_context.Users.ToList()[0].Credentials.Logins.ToList().Find(l => l.Type == LoginType.Email).Value,
                _context.Emails.ToList()[1].Value);
        }
    }
}
