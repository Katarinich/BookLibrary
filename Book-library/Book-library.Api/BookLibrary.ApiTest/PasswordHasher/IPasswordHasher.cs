namespace BookLibrary.ApiTest
{
    interface IPasswordHasher
    {
        string GetHash(string source);
    }
}
