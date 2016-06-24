namespace BookLibrary.Api
{
    public interface IPasswordChangeService
    {
        void ChangePassword(int userId, string oldPasswordValue, string newPasswordValue);
    }
}