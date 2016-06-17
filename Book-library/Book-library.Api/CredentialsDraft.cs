namespace BookLibrary.Api
{
    public class CredentialsDraft
    {
        public CredentialsDraft(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public string login { get; set; }
        public string password { get; set; }
    }
}
