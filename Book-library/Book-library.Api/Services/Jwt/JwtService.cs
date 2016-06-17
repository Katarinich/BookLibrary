using System;
using System.Collections.Generic;
using Jose;
using BookLibrary.Api.Models;
using BookLibrary.Api.Managers;

namespace BookLibrary.Api.Services
{
    public class JwtService : IJwtService
    {
        private JwsAlgorithm _algortithm;
        private byte[] _secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
        private ITokenManager _tokenManager;
        public JwtService(JwsAlgorithm algortithm, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _algortithm = algortithm;
        }

        public Token CreateToken(User user)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.UtcNow.AddHours(2) - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                {"userId", user.UserId},
                {"sub", user.UserId},
                {"exp", expiry}
            };

            var token = new Token();
            token.Value = JWT.Encode(payload, _secretKey, _algortithm); ;
            token.ExpirationDate = unixEpoch;

            _tokenManager.AddToken(token);

            return token;
        }

        public bool ValidateToken(string tokenValue)
        {
            var token = _tokenManager.GetTokenByValue(tokenValue);
            if (token == null) return false;
            if (DateTime.Compare(DateTime.UtcNow, token.ExpirationDate) > 0)
            {
                return false;
            }
            return true;
        }
    }
}