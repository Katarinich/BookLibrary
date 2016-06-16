using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.ApiTest
{
    interface IPasswordPolicy
    {
        bool SatisfiesPolicy(User user, string newPasswordValue);
    }
}