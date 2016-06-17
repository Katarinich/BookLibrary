namespace BookLibrary.Api
{
    interface IPasswordChangeService
    {
        void ChangePassword(int userId, string oldPasswordValue, string newPasswordValue);
    }
}