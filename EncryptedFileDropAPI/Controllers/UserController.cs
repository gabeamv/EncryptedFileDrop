using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EncryptedFileDropAPI.Models;
using EncryptedFileDropAPI.Data;

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

        // Adds user to the database.
        [HttpPost]
        public JsonResult Register(User user) 
        { 
            
            if (user.Id == 0) // If the user's id is null
            {
                
                _context.Users.Add(user); // We add the user to the database
            } else // If the user's id is not null
            {
                // We check if the user is in the database.
                var userInDb = _context.Users.Find(user.Id);

                // If the user is not in the db, return not found.
                if (userInDb == null) return new JsonResult(NotFound());

            }
            // Save the database.
            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }
    }
}
