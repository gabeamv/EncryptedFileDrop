using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EncryptedFileDropAPI.Models;
using EncryptedFileDropAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EncryptedFileDropAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EncryptedFileDropContext _context;

        // Inject the db context into the controller.
        public UserController(EncryptedFileDropContext context) 
        {
            _context = context;
        }

        // Get the user from the database by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // We find a user by ID from the database.
            var user = await _context.Users.FindAsync(id);

            // If what is returned is null, no user has been found.
            if (user == null)
            {
                return NotFound();
            }
            // Return the user.
            return user;
        }

        // Adds user to the database.
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(User user) 
        {
            // Check for duplicate usernames in the databse.
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                return Conflict("Username already exists");
            }

            // Add the user to the database.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return the location url of the newly created resource, as well as the created object.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}
