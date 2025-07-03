using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EncryptedFileDropAPI.Models;
using EncryptedFileDropAPI.Data;
using EncryptedFileDropAPI.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Mapster;

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
            return Ok(user);
        }

        // Adds user to the database.
        // TODO: Implement hashing of the user's password.
        [HttpPost("register")]
        public async Task<ActionResult<UserLoginResponse>> RegisterUser(User user) 
        {
            // Check for duplicate usernames in the databse.
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                return Conflict("Username already exists");
            }

            // Add the user to the database.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Get the response for the user logging in
            var response = user.Adapt<UserLoginResponse>();

            // Send the response to the client that the user has successfully registered.
            return Ok(response);
        }

        // Logs the user into the database.
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> LoginUser(UserLoginDto userLogin)
        {
            // Search the database for the first matching record using the inputted user name from the user.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userLogin.UserName);

            // If user does not exist or the hash of the inputted password does not match with the hash of the stored
            // password, user is unauthorized to log-in.

            // TODO: Implement hashing of the user's inputted password.
            if (user == null || userLogin.PasswordHash != user.PasswordHash)
            {
                return Unauthorized("User inputted incorrect credentials.");
            }

            // Send the response to the client so that the user can log in.
            var response = user.Adapt<UserLoginResponse>();
            return Ok(response);
        }

    }
}
