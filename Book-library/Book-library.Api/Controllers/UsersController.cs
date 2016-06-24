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
using BookLibrary.Api.Exceptions.PasswordExceptions;

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
        private IPasswordChangeService _passwordChangeService;
        private IPasswordRecoveryService _passwordRecoveryService;

        public UsersController(IUserService userService, IRegistrationService registrationalService, IConfirmationSenderService confirmationalService,
            IAuthentificationService authentificationSerice, IJwtService jwtService, IEmailChangeService emailChangeService, IEmailConfirmationService emailConfirmationService,
            IPasswordChangeService passwordChangeService, IPasswordRecoveryService passwordRecoveryService)
        {
            _emailChangeService = emailChangeService;
            _userService = userService;
            _jwtService = jwtService;
            _autentificationService = authentificationSerice;
            _registrationService = registrationalService;
            _confirmationSenderService = confirmationalService;
            _emailConfirmationService = emailConfirmationService;
            _passwordChangeService = passwordChangeService;
            _passwordRecoveryService = passwordRecoveryService;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers(HttpRequestMessage request)
        {
            if (request.Headers.Authorization == null || !_jwtService.ValidateToken("ololo"))
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Not authorized!");


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

            return Request.CreateResponse(HttpStatusCode.OK, usersDTO);
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

        [Route("password/change")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(JObject passwords)
        {
            var userId = (int)passwords["userId"];
            var oldPasswordValue = passwords["oldPasswordValue"].ToString();
            var newPasswordValue = passwords["newPasswordValue"].ToString();

            try
            {
                _passwordChangeService.ChangePassword(userId, oldPasswordValue, newPasswordValue);
            }
            catch(OldPasswordWrongException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch(PasswordDoesNotSatisfyPolicyException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Password was changed.");
        }

        [Route("password/recover/initiate")]
        [HttpPost]
        public HttpResponseMessage InitiatePasswordRecovery(JObject email)
        {
            var emailValue = email["emailValue"].ToString();

            try
            {
                _confirmationSenderService.SendConfirmation(emailValue, ConfirmationCodeType.PasswordRecovery);
            }
            catch(EmailNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Email not found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Email with confirmation was sended on your email address.");
        }

        [Route("password/recover/finish")]
        [HttpPost]
        public HttpResponseMessage FinishPasswordRecovery(JObject passwordRecoveryDraft)
        {
            var newPasswordValue = passwordRecoveryDraft["newPasswordValue"].ToString();
            var codeValue = passwordRecoveryDraft["codeValue"].ToString();
            try
            {
                _passwordRecoveryService.RecoverPassword(newPasswordValue, codeValue);
            }
            catch (PasswordDoesNotSatisfyPolicyException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception ex)
                when (ex is CodeIsNotActiveException || ex is CodeIsNotExistException || ex is CodeExpirationDateIsUpException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Code is not valid.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Password was changed.");
        }
    }
}
