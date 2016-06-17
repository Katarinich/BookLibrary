using BookLibrary.ApiTest.Managers;
using System;

namespace BookLibrary.ApiTest
{
    class EmailConfirmationTaker
    {
        public static bool TakeConfirmation(string codeValue, string emailValue)
        {
            var codeManager = new ConfirmationCodeManager();
            var code = codeManager.GetConfirmationCodeByValueAndEmail(codeValue, emailValue);

            if (!code.IsActive) return false;
            if (DateTime.Now.Subtract(code.ExpirationDate.DateTime).Seconds > 0) return false;

            var emailManager = new EmailManager();
            var userManager = new UserManager();
            var user = userManager.GetUserByLogin(emailValue);


            emailManager.SetEmailConfirmed(user);

            codeManager.SetCodeNotActive(code);
            return true;
        }
    }
}
