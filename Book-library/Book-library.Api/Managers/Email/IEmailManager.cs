using BookLibrary.Api.Models;

namespace BookLibrary.Api.Managers
{
    public interface IEmailManager
    {
        Email GetEmailByValue(string emailValue);
        void SaveEmail(string emailValue);
    }
}
