namespace EncryptedFileDropAPI.DTOs.User
{
    // User receives id and username when logging in.
    public class UserLoginResponse
    {

        // TODO: Shared DTO between client and backend. Can we make this more seamless and cleaner?
        public int Id { get; set; }
        public required string UserName { get; set; }
    }
}
