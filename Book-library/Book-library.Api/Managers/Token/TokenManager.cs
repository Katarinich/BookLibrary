﻿using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Api.Managers
{
    public class TokenManager : ITokenManager
    {
        private BookLibraryContext _context;

        public TokenManager(BookLibraryContext context)
        {
            _context = context;
        }

        public void AddToken(Token token)
        {
            _context.Tokens.Add(token);
            _context.SaveChanges();
        }

        public Token GetTokenByValue(string tokenValue)
        {
            return _context.Tokens.ToList().FirstOrDefault(t => t.Value == tokenValue);
        }
    }
}