namespace BookLibrary.Api
{
    interface IPasswordHasher
    {
        string GetHash(string source);
    }
}
