namespace BookLibrary.Api.Models
{
    public class EmailChangeRequest
    {
        public User User { get; set; }
        public string AwaitingEmail { get; set; }
        public Email ActiveEmail { get; set; }
        public ConfirmationCode Code { get; set; }
    }
}