using EncryptedFileDropAPI.Data;
using Microsoft.AspNetCore.Mvc;
using EncryptedFileDropAPI.Models;

namespace EncryptedFileDropAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly EncryptedFileDropContext _context;
        public KeyController(EncryptedFileDropContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddKey(Key key) 
        {
            _context.Keys.Add(key);
            int saved = await _context.SaveChangesAsync();
            return Ok(key);
        }

    }
}
