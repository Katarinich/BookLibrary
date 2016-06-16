using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using System.Linq;

namespace BookLibrary.ApiTest.Managers
{
    class EmailManager : IEmailManager
    {
        private BookLibraryContext _context;

        public EmailManager(BookLibraryContext context)
        {
            _context = context;
        }

        //public EmailManager()
        //{
        //    _context = new BookLibraryContext();
        //}

        public Email GetEmailByValue(string emailValue)
        {
            return _context.Emails.FirstOrDefault(e => e.Value == emailValue);
        }

        public void SaveEmail(string emailValue)
        {
            var email = new Email();
            email.Value = emailValue;
            email.IsConfirmed = false;
            _context.Emails.Add(email);
            _context.SaveChanges();
        }
    }
}
