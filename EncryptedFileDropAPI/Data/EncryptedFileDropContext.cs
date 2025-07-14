using Microsoft.EntityFrameworkCore;
using EncryptedFileDropAPI.Models;

namespace EncryptedFileDropAPI.Data
{
    // Inherit APIContext from DbContext
    public class EncryptedFileDropContext: DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public EncryptedFileDropContext(DbContextOptions<EncryptedFileDropContext> options): 
            base(options) 
        { 

        }
    }
}
