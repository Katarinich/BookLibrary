﻿using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.Api
{
    public interface IConfirmationCodeManager
    {
        void SaveCode(ConfirmationCode code, Email email);

        ConfirmationCode GetConfirmationCodeByValue(string codeValue);

        List<ConfirmationCode> GetConfirmationCodesByUserId(int userId);

        void UpdateCode();
    }
}