using EimakShas.Data;
using EimakShas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Please fill in the required fields.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok($"{user.FirstName} {user.LastName} has succesfully been added.");
        }

        public UserController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
    }
}
