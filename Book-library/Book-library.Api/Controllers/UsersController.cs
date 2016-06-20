using BookLibrary.Api.DAL;
using BookLibrary.Api.Managers;
using BookLibrary.Api.Models;
using BookLibrary.Api.Services;
using BookLibrary.Api.Services.NotificationTransport;
using SimpleInjector;
using System.Web.Http;

namespace BookLibrary.Api.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private IUserService _userService;
        private IRegistrationService _registrationService;
        private IConfirmationSenderService _confirmationSenderService;
        private IAuthentificationService _autentificationService;
        private IJwtService _jwtService;

        public UsersController()
        {
            var container = new Container();
            container.Register<BookLibraryContext, BookLibraryContext>(Lifestyle.Singleton);
            container.Register<IUserManager, UserManager>(Lifestyle.Singleton);
            container.Register<IConfirmationCodeManager, ConfirmationCodeManager>(Lifestyle.Singleton);
            container.Register<IEmailManager, EmailManager>(Lifestyle.Singleton);
            container.Register<ICodeGenerator, CodeGenerator>();
            container.Register<INotificationTransportService, NotificationTransportService>();

            _userService = container.GetInstance<UserService>();
            _registrationService = container.GetInstance<RegistrationService>();
            _confirmationSenderService = container.GetInstance<ConfirmationSenderService>();
        }

        [Route("signup")]
        [HttpPost]
        public IHttpActionResult Register(UserDraft userDraft)
        {
            string[] logins = { userDraft.email, userDraft.userName, userDraft.mobilePhone };

            var existingUser = _userService.GetUserByLogins(logins);

            if (existingUser != null)
            {
                return BadRequest("User already exist.");
            }

            var userBuilder = new UserBuilder();
            var user = userBuilder.BuildUser(userDraft);

            _registrationService.RegisterUser(user);

            _confirmationSenderService.SendConfirmation(userDraft.email, ConfirmationCodeType.EmailConfirmation);

            return Ok("Registration has complete.");
        }

        [Route("signin")]
        public IHttpActionResult Login(CredentialsDraft credentialsDraft)
        {
            var user = _autentificationService.Authentificate(credentialsDraft);

            if (user == null) return BadRequest("User not found.");

            var token = _jwtService.CreateToken(user);

            return Ok(new { user, token.Value });
        }
    }
}
