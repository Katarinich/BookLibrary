namespace BookLibrary.Api.Services
{
    interface IPasswordRecoveryService
    {
        void RecoverPassword(string newPasswordValue, string codeValue);
    }
}