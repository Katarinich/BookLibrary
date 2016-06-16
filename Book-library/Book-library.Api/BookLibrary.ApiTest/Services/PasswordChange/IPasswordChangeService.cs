namespace BookLibrary.ApiTest
{
    interface IPasswordChangeService
    {
        void ChangePassword(int userId, string oldPasswordValue, string newPasswordValue);
    }
}