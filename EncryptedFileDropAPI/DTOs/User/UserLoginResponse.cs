namespace EncryptedFileDropAPI.DTOs.User
{
    // User receives id and username when logging in.
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
    }
}
