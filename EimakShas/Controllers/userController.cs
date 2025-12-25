using EimakShas.Data;
using EimakShas.DTOs;
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
            var users = _context.Users.Include(u => u.Chavrisa).ToList();
            var usersDTO = new List<GetUsersDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new GetUsersDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    DafimAmount = user.DafimAmount,
                    DafimCompleted = user.DafimFinished,
                    DafimNotCompleted = user.DafimAmount - user.DafimFinished,
                    PercentageCompleted = user.PercentageFinished,
                    DafPerDay = user.DafPerDay,
                    HasText = user.HasText,
                    ChavrisaId = user.ChavrisaId,
                    ChavrisaName = user.Chavrisa.FirstName + " " + user.Chavrisa.LastName
                });

            }
            return Ok(usersDTO);
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
