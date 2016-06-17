using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.Api
{
    interface IPasswordPolicy
    {
        bool SatisfiesPolicy(User user, string newPasswordValue);
    }
}