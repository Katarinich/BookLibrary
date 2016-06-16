using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Managers
{
    public interface IEmailManager
    {
        Email GetEmailByValue(string emailValue);
        void SaveEmail(string emailValue);
    }
}
