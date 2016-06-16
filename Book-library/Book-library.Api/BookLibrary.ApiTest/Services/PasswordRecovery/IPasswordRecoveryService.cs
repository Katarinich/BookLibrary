namespace BookLibrary.ApiTest.Services
{
    interface IPasswordRecoveryService
    {
        void RecoverPassword(string newPasswordValue, string codeValue);
    }
}