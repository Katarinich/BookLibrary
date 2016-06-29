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
using BookLibrary.Api.Services.Validation;

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
        private IValidationService _userDraftValidationService;

        public UsersController(IUserService userService, IRegistrationService registrationalService, IConfirmationSenderService confirmationalService,
            IAuthentificationService authentificationSerice, IJwtService jwtService, IEmailChangeService emailChangeService, IEmailConfirmationService emailConfirmationService,
            IPasswordChangeService passwordChangeService, IPasswordRecoveryService passwordRecoveryService, IValidationService userDraftValidationService)
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
            _userDraftValidationService = userDraftValidationService;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers(HttpRequestMessage request)
        {
            if (request.Headers.Authorization == null || !_jwtService.ValidateToken(request.Headers.Authorization.Scheme))
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Not authorized!");

            var tokenValue = request.Headers.Authorization.Scheme;

            var users = _userService.GetAllUsers();
            List<UserDTO> usersDTO = new List<UserDTO>();

            var userId = _jwtService.GetUserIdFromToken(tokenValue);
            var token = _jwtService.CreateToken(userId);

            foreach (var user in users)
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

            return Request.CreateResponse(HttpStatusCode.OK, new { users = usersDTO, token = token });
        }

        [Route("signup")]
        [HttpPost]
        public HttpResponseMessage Register(JObject userObject)
        {
            IList<string> errorMessages;

            if (!_userDraftValidationService.IsValid(userObject, out errorMessages))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, errorMessages);
            }

            var userDraft = userObject.ToObject<UserDraft>();

            string[] logins = { userDraft.email, userDraft.userName, userDraft.mobilePhone };

            try
            {
                _userService.CheckIfUserExistByLogin(logins);
            }
            catch(UserAlreadyExistException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var userBuilder = new UserBuilder();
            var user = userBuilder.BuildUser(userDraft);

            if(!_registrationService.TryRegisterUser(user))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User with this email already exist");
            }

            _confirmationSenderService.SendConfirmation(userDraft.email, ConfirmationCodeType.EmailConfirmation);

            return Request.CreateResponse(HttpStatusCode.OK, "Registration has complete. Email with confiramtion code was sended on your email address. Please, confirm you address to login in system.");
        }

        [Route("signin")]
        public HttpResponseMessage Login(CredentialsDraft credentialsDraft)
        {
            var user = _autentificationService.Authentificate(credentialsDraft);

            if (user == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "Wrong login or password.");
            else if (user.Email == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "Sorry, you can't login. You didn't confirm your email address.");

            var token = _jwtService.CreateToken(user.UserId);

            var userDTO = new UserDTO()
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PendingEmail = user.PendingEmail,
                MobilePhone = user.MobilePhone.Value,
                DateOfBirth = (int)user.DateOfBirth.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds,
                Country = user.Address.Country,
                State = user.Address.State,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                AddressLine = user.Address.AddressLine,
                Roles = user.UserRoles.Select(x => x.Name).ToArray()
            };


            var response = Request.CreateResponse(HttpStatusCode.OK, new { user = userDTO, token = token });

            return response;
        }

        [Route("update")]
        public HttpResponseMessage UpdateUser(HttpRequestMessage request, JObject userObject)
        {
            if (request.Headers.Authorization == null || !_jwtService.ValidateToken(request.Headers.Authorization.Scheme))
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Not authorized!");

            IList<string> errorMessages;

            if (!_userDraftValidationService.IsValid(userObject, out errorMessages))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, errorMessages);
            }

            var userDraft = userObject.ToObject<UserDraft>();

            var tokenValue = request.Headers.Authorization.Scheme;
            var userId = _jwtService.GetUserIdFromToken(tokenValue);
            var token = _jwtService.CreateToken(userId);

            var userBuilder = new UserBuilder();
            var updatedUser = userBuilder.BuildUser(userDraft);
            var userToUpdate = _userService.GetUserByLogin(updatedUser.Emails.First().Value);

            if(userToUpdate.UserName != updatedUser.UserName ) 
            {
                string[] logins = { userDraft.userName };

                try
                {
                    _userService.CheckIfUserExistByLogin(logins);
                }
                catch (UserAlreadyExistException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message, token = token });
                }
            }

            if(userToUpdate.MobilePhone.Value != updatedUser.MobilePhone.Value)
            {
                string[] logins = { userDraft.mobilePhone };

                try
                {
                    _userService.CheckIfUserExistByLogin(logins);
                }
                catch (UserAlreadyExistException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message, token = token });
                }
            }

            User user = _userService.UpdateUser(userToUpdate, updatedUser);

            var userDTO = new UserDTO()
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
                AddressLine = user.Address.AddressLine,
                Roles = user.UserRoles.Select(x => x.Name).ToArray()
            };

            return Request.CreateResponse(HttpStatusCode.OK, new { user = userDTO, token = token });
        }

        [Route("email/confirm/{codeValue}")]
        [HttpGet]
        public HttpResponseMessage ConfirmEmail(string codeValue)
        {
            if (!_emailConfirmationService.TryAcceptConfirmation(codeValue, ConfirmationCodeType.EmailConfirmation))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Code is not valid.");
            return Request.CreateResponse(HttpStatusCode.OK, "Email was confirmed.");
        } 

        [Route("email/change/initiate")]
        [HttpPost]
        public HttpResponseMessage InitiateEmailChange(HttpRequestMessage request, JObject user)
        {
            if (request.Headers.Authorization == null || !_jwtService.ValidateToken(request.Headers.Authorization.Scheme))
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Not authorized!");

            IList<string> errorMessages;
            var userId = (int)user["userId"];
            var newEmailValue = user["email"].ToString();
            var token = _jwtService.CreateToken(userId);

            if (!_userDraftValidationService.IsValid(user, out errorMessages))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Email is invalid.", token = token });
            }

            try
            {
                _emailChangeService.InitiateChangeEmailProcess(userId, newEmailValue);
            }
            catch (EmailIsAlredyTakenException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message, token = token });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Email with confirmation was sended on your current email address.", token = token, pendingEmail = newEmailValue });
        }

        [Route("email/change/continue/{codeValue}")]
        [HttpGet]
        public HttpResponseMessage ContinueEmailChange(string codeValue)
        {
            if (!_emailChangeService.TrySendConfirmationToNewEmail(codeValue))
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
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, JObject passwords)
        {
            if (request.Headers.Authorization == null || !_jwtService.ValidateToken(request.Headers.Authorization.Scheme))
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Not authorized!");

            var userId = (int)passwords["userId"];
            var oldPasswordValue = passwords["oldPasswordValue"].ToString();
            var newPasswordValue = passwords["newPasswordValue"].ToString();
            var token = _jwtService.CreateToken(userId);

            try
            {
                _passwordChangeService.ChangePassword(userId, oldPasswordValue, newPasswordValue);
            }
            catch(OldPasswordWrongException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message, token = token });
            }
            catch(PasswordDoesNotSatisfyPolicyException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message, token = token });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Password was changed.", token = token });
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
