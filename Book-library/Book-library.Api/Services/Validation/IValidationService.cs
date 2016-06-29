using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BookLibrary.Api.Services.Validation
{
    public interface IValidationService
    {
        bool IsValid(JObject jObject, out IList<string> errorMessages);
    }
}
