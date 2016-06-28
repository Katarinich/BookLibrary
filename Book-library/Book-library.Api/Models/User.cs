using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BookLibrary.Api.Models
{
    public class User
    {
        public User()
        {
            UserRoles = new List<Role>();
            Emails = new List<Email>();
        }

        [Key]
        public int UserId { get; set; }
        public Credentials Credentials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Email> Emails { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public MobilePhone MobilePhone { get; set; }
        public List<Role> UserRoles { get; set; }

        public string Email => Emails.Find(e => e.IsActive && e.IsConfirmed)?.Value;
        public string PendingEmail => Emails.FindLast(x => x.IsConfirmed == false)?.Value;

        public void ChangeEmailTo(string newEmailValue)
        {
            if (Emails.Count > 1)
            {
                Emails.Find(e => e.IsActive).IsActive = false;
            }

            Emails.Find(e => e.Value == newEmailValue).IsActive = true;

            Credentials.Logins.Find(l => l.Type == LoginType.Email).Value = newEmailValue;
        }

        public void AddEmail(Email email)
        {
            email.User = this;
            Emails.Add(email);
        }

        public void UpdatePassword(string passwordValue)
        {
            var oldPassword = Credentials.Passwords.First(p => p.IsActive);
            oldPassword.IsActive = false;

            var newPassword = new Password();
            newPassword.Value = passwordValue;
            newPassword.IsActive = true;
            newPassword.ExpirationDate = DateTimeOffset.Now.AddYears(1);
            newPassword.Credentials = Credentials;

            Credentials.Passwords.Add(newPassword);
        }
    }
}