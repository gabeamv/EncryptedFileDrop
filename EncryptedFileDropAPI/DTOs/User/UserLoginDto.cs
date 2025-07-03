namespace EncryptedFileDropAPI.DTOs.User
{
    // User enters username and password, which will be hashed by the server later.
    public class UserLoginDto
    {
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
    }
}
