namespace EncryptedFileDropAPI.Models
{
    // Model for the user.
    public class User
    {
        // Primary key is the ID of the user.
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
    }
}
