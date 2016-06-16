using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services
{
    interface ILoginService
    {
        User Authentification(CredentialsDraft credentialsDraft);
    }
}
