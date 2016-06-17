using System;

namespace BookLibrary.Api
{
    class RandomUserDraftGenerator : IUserDraftGenerator
    {
        public UserDraft GenerateUserDraft()
        {
            var userDraft = new UserDraft();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string charsForName = "abcdefghijklmnopqrstuvwxyz";
            const string numericChars = "1234567890";
            var start = new DateTime(1946, 1, 1);

            var generator = new Generator();

            userDraft.userName = generator.GenerateString(chars);

            userDraft.firstName = generator.GenerateString(charsForName);
            userDraft.firstName = char.ToUpper(userDraft.firstName[0]) + userDraft.firstName.Substring(1);

            userDraft.lastName = generator.GenerateString(charsForName);
            userDraft.lastName = char.ToUpper(userDraft.lastName[0]) + userDraft.lastName.Substring(1);

            userDraft.email = userDraft.firstName.ToLower() + "." + userDraft.lastName.ToLower() + "@gmail.com";
            userDraft.password = "123456";
            userDraft.dateOfBirth = DateTime.Now.Subtract(generator.GenerateDate(start)).TotalSeconds.ToString();
            userDraft.mobilePhone = "+" + generator.GenerateString(numericChars);

            userDraft.country = generator.GenerateString(charsForName);
            userDraft.country = char.ToUpper(userDraft.country[0]) + userDraft.country.Substring(1);

            userDraft.state = generator.GenerateString(charsForName);
            userDraft.state = char.ToUpper(userDraft.state[0]) + userDraft.state.Substring(1);

            userDraft.city = generator.GenerateString(charsForName);
            userDraft.city = char.ToUpper(userDraft.city[0]) + userDraft.city.Substring(1);

            userDraft.addresLine = generator.GenerateString(chars);
            userDraft.zipCode = generator.GenerateString(numericChars);

            return userDraft;
        }
    }
}
