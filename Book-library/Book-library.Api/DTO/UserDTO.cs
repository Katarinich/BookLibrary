namespace BookLibrary.Api.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string TokenValue { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        public string UserName { get; set; }
    }
}