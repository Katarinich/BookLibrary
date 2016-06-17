using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using BookLibrary.Api.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLibrary.Api.Controllers
{
    [RoutePrefix("auth")]
    public class AccountControler : ApiController
    {
        private BookLibraryContext db = new BookLibraryContext();
        private IUserService _userService;
        private IRegistrationService _registrationService;
        private IConfirmationSenderService _confirmationSenderService;
        private IAuthentificationService _autentificationService;
        private IJwtService _jwtService;

        [Route("signup")]
        [HttpPost]
        public HttpResponseMessage Register(UserDraft userDraft)
        {
            string[] logins = { userDraft.email, userDraft.userName, userDraft.mobilePhone };

            var existingUser = _userService.GetUserByLogins(logins);

            if (existingUser != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User already exist.");
            }

            var userBuilder = new UserBuilder();
            var user = userBuilder.BuildUser(userDraft);

            _registrationService.RegisterUser(user);

            _confirmationSenderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailConfirmation);

            return Request.CreateResponse(HttpStatusCode.OK, "Registration has complete.");
        }

        [Route("signin")]
        public IHttpActionResult Login(CredentialsDraft credentialsDraft)
        {
            var user = _autentificationService.Authentificate(credentialsDraft);

            if(user == null ) return BadRequest("User not found.");

            var token = _jwtService.CreateToken(user);

            return Ok(new { user, token.Value});
        }
    }
}