﻿namespace BookLibrary.Api.Services
{
    class PasswordRecoveryService : IPasswordRecoveryService
    {
        private IConfirmationCodeService _confirmationCodeService;
        private IPasswordPolicy _passwordPolicy;
        private IPasswordHasher _passwordHasher;

        public PasswordRecoveryService(IConfirmationCodeService confirmationCodeService, 
            IPasswordPolicy passwordPolicy, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _confirmationCodeService = confirmationCodeService;
            _passwordPolicy = passwordPolicy;
        }

        public void RecoverPassword(string newPasswordValue, string codeValue)
        {
            _confirmationCodeService.ValidateCode(codeValue, Models.ConfirmationCodeType.PasswordRecovery);
            
            var user = _confirmationCodeService.GetRelatedUser(codeValue);

            if (!_passwordPolicy.SatisfiesPolicy(user, newPasswordValue))
                throw new PasswordDoesNotSatisfyPolicyException("Password does not satisfy policy.");

            newPasswordValue = _passwordHasher.GetHash(newPasswordValue);
            user.UpdatePassword(newPasswordValue);
            _confirmationCodeService.DeactivateCode(codeValue);
        }
    }
}
