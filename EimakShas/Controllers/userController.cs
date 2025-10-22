using EimakShas.Data;
using EimakShas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Controllers
{
    public class userController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        [HttpPost]
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

        public userController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

    }
}
