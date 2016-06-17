using System;

namespace BookLibrary.Api.Exceptions.PasswordExceptions
{
    class NewPasswordDoesNotApplyPolicyException : Exception
    {
        public NewPasswordDoesNotApplyPolicyException(string message)
            :base(message)
        {

        }
    }
}
