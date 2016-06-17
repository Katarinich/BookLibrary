using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IAuthentificationService
    {
        User Authentificate(CredentialsDraft credentialsDraft);
    }
}
