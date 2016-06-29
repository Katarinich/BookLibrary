using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;

namespace BookLibrary.Api.Services.Validation
{
    public class UserDraftValidationService : IValidationService
    {
        private static int maxDateOfBirth = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        private string schemaString = @"{
            'type': 'object',
            'properties': {
                'userName': { 'type': 'string', 'pattern': '^[a-zA-Z0-9_-]{3,16}$', 'messages': { 'pattern': 'UserName can contain only letters and numbers.'} },
                'firstName': { 'type': 'string', 'pattern': '^.+$', 'messages': { 'pattern': 'First name can not be empty.'} },
                'lastName': { 'type': 'string', 'pattern': '^.+$', 'messages': { 'pattern': 'Last name can not be empty.'} },
                'email': { 'type': 'string', 'pattern':'^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$', 'messages': { 'pattern': 'Email is not valid.'} },
                'dateOfBirth': { 'type': 'integer', 'maximum':" + maxDateOfBirth + @", 'messages': { 'maximum': 'Date of birth is not valid.'} },
                'mobilePhone': { 'type': 'string', 'pattern': '^(\\+|\\d)[0-9]{7,16}$', 'messages': { 'pattern': 'Mobile phone is not valid.'} },
                'country': { 'type': 'string', 'pattern': '^.+$', 'messages': { 'pattern': 'Country can contain only letters.'} },
                'state': { 'type': 'string'},
                'city': { 'type': 'string', 'pattern': '^.+$', 'messages': { 'pattern': 'City can contain only letters.'} },
                'addressLine': { 'type': 'string', 'pattern': '^.+$'},
                'zipcode': { 'type': 'string', 'pattern': '^.+$'},
                'password': { 'type': 'string', 'pattern': '^[a-zA-Z0-9_-]{8,18}$', 'messages': { 'pattern': 'Password can contain only letters and numbers and contains from 8 to 18 characters.'} }
                }
        }";

        private readonly JSchema schema;
        public UserDraftValidationService()
        {
            schema = JSchema.Parse(schemaString);
        }

        public bool IsValid(JObject userDraftJObject, out IList<string> errorMessages)
        {
            return userDraftJObject.IsValid(schema, out errorMessages);
        }
    }
}