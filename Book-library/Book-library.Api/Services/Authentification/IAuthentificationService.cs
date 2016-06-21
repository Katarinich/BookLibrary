using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IAuthentificationService
    {
        User Authentificate(CredentialsDraft credentialsDraft);
    }
}
