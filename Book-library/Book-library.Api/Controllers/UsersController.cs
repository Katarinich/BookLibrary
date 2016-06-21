using Book_library.Api.DTO;
using BookLibrary.Api.Models;
using BookLibrary.Api.Services;
using System;
using System.Net;
using System.Net.Http;
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

        public UsersController(IUserService userService, IRegistrationService registrationalService, IConfirmationSenderService confirmationalService, 
            IAuthentificationService authentificationSerice, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _autentificationService = authentificationSerice;
            _registrationService = registrationalService;
            _confirmationSenderService = confirmationalService;
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
        public HttpResponseMessage Login(CredentialsDraft credentialsDraft)
        {
            var user = _autentificationService.Authentificate(credentialsDraft);

            if (user == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

            var token = _jwtService.CreateToken(user);

            var userDTO = new UserDTO();

            userDTO.Id = user.UserId;
            userDTO.TokenValue = token.Value;

            var response = Request.CreateResponse(HttpStatusCode.OK, userDTO);

            return response;
        }
    }
}
