namespace BookLibrary.Api.Services
{
    public interface IPasswordRecoveryService
    {
        void RecoverPassword(string newPasswordValue, string codeValue);
    }
}