using System;

namespace BookLibrary.ApiTest.Exceptions.PasswordExceptions
{
    class NewPasswordDoesNotApplyPolicyException : Exception
    {
        public NewPasswordDoesNotApplyPolicyException(string message)
            :base(message)
        {

        }
    }
}
