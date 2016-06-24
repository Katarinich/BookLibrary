using BookLibrary.Api.DTO;
using BookLibrary.Api.Models;
using BookLibrary.Api.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Newtonsoft.Json.Linq;
using BookLibrary.Api.Exceptions;
using BookLibrary.Api.Exceptions.CodeExceptions;

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
        private IEmailChangeService _emailChangeService;
        private IEmailConfirmationService _emailConfirmationService;

        public UsersController(IUserService userService, IRegistrationService registrationalService, IConfirmationSenderService confirmationalService, 
            IAuthentificationService authentificationSerice, IJwtService jwtService, IEmailChangeService emailChangeService, IEmailConfirmationService emailConfirmationService)
        {
            _emailChangeService = emailChangeService;
            _userService = userService;
            _jwtService = jwtService;
            _autentificationService = authentificationSerice;
            _registrationService = registrationalService;
            _confirmationSenderService = confirmationalService;
            _emailConfirmationService = emailConfirmationService;
        }

        [Route("")]
        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach(var user in users)
            {
                usersDTO.Add(new UserDTO()
                {
                    Id = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    MobilePhone = user.MobilePhone.Value,
                    DateOfBirth = (int)user.DateOfBirth.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds,
                    Country = user.Address.Country,
                    State = user.Address.State,
                    City = user.Address.City,
                    Zipcode = user.Address.Zipcode,
                    AddressLine = user.Address.AddressLine
                });
            }

            return usersDTO;
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

            var userDTO = new UserDTO()
            {
                Id = user.UserId,
                TokenValue = token.Value,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                MobilePhone = user.MobilePhone.Value,
                DateOfBirth = (int)user.DateOfBirth.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds,
                Country = user.Address.Country,
                State = user.Address.State,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                AddressLine = user.Address.AddressLine,
                Roles = user.UserRoles.Select(x => x.Name).ToArray()
            };


            var response = Request.CreateResponse(HttpStatusCode.OK, userDTO);

            return response;
        }

        [Route("logout")]
        public IHttpActionResult Logout(UserDTO userDTO)
        {
            _jwtService.DeactivateToken(userDTO.TokenValue);

            return Ok("Logout was successfull.");
        }

        [Route("update")]
        public HttpResponseMessage UpdateUser(UserDraft userDraft)
        {
            var userBuilder = new UserBuilder();
            var updatedUser = userBuilder.BuildUser(userDraft);
            var user = _userService.UpdateUser(updatedUser);

            var token = _jwtService.CreateToken(user);

            var userDTO = new UserDTO()
            {
                Id = user.UserId,
                TokenValue = token.Value,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                MobilePhone = user.MobilePhone.Value,
                DateOfBirth = (int)user.DateOfBirth.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds,
                Country = user.Address.Country,
                State = user.Address.State,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                AddressLine = user.Address.AddressLine,
                Roles = user.UserRoles.Select(x => x.Name).ToArray()
            };

            return Request.CreateResponse(HttpStatusCode.OK, userDTO);
        }

        [Route("email/confirm/{codeValue}")]
        [HttpGet]
        public HttpResponseMessage ConfirmEmail(string codeValue)
        {
            if (!_emailConfirmationService.TryAcceptConfirmation(codeValue))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Code is not valid.");
            return Request.CreateResponse(HttpStatusCode.OK, "Email was confirmed.");
        } 

        [Route("email/change/initiate")]
        [HttpPost]
        public HttpResponseMessage InitiateEmailChange(JObject user)
        {
            var userId = (int)user["userId"];
            var newEmailValue = user["newEmailValue"].ToString();

            try
            {
                _emailChangeService.InitiateChangeEmailProcess(userId, newEmailValue);
            }
            catch (EmailIsAlredyTakenException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Email with confirmation was sended on your current email address.");
        }

        [Route("email/change/continue/{codeValue}")]
        [HttpGet]
        public HttpResponseMessage ContinueEmailChange(string codeValue)
        {
            if(!_emailChangeService.TrySendConfirmationToNewEmail(codeValue))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Code is not valid.");
            return Request.CreateResponse(HttpStatusCode.OK, "Email with confirmation was sended on your new email address.");
        }

        [Route("email/change/finish/{codeValue}")]
        [HttpGet]
        public HttpResponseMessage FinishEmailChange(string codeValue)
        {
            string newEmailValue;

            try
            {
                newEmailValue = _emailChangeService.ChangeEmail(codeValue);
            }
            catch(CodeIsNotValidException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Code is not valid.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, newEmailValue);
        }
    }
}
