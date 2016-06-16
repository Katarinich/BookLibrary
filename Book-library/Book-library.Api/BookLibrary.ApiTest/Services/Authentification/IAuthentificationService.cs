using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services
{
    interface IAuthentificationService
    {
        User Authentificate(CredentialsDraft credentialsDraft);
    }
}
