using EimakShas.Data;
using EimakShas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbContext;

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("userMasechtas")]
        public async Task<IActionResult> GetUserMasechtas()
        {
            var userMasechtas = await _context.UserUmidim.ToListAsync();

            return Ok(userMasechtas);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest(new { message = "Please fill in the required fields."});
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("Delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
