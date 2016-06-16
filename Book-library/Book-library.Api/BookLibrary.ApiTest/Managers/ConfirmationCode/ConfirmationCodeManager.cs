using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using System.Linq;

namespace BookLibrary.ApiTest.Managers
{
    class ConfirmationCodeManager : IConfirmationCodeManager
    {
        private BookLibraryContext _context;

        public ConfirmationCodeManager(BookLibraryContext context)
        {
            _context = context;
        }

        public void SaveCode(ConfirmationCode code, Email email)
        {
            code.Email = email;
            _context.Codes.Add(code);
            _context.SaveChanges();
        }

        public ConfirmationCode GetConfirmationCodeByValue(string codeValue)
        {
            return _context.Codes
                .Include("Email")
                .Include("Email.User")
                .Include("Email.User.Emails")
                .Include("Email.User.Credentials")
                .Include("Email.User.Credentials.Passwords")
                .FirstOrDefault(c => c.Value == codeValue);
        }

        public void UpdateCode()
        {
            _context.SaveChanges();
        }
    }
}
