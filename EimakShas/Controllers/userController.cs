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
                    ChavrisaName = user.Chavrisa != null
                ? $"{user.Chavrisa.FirstName} {user.Chavrisa.LastName}"
                : null
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
                return BadRequest(new { message = "Please fill in the required fields." });
            }

            if (user.ChavrisaId == 0)
            {
                user.ChavrisaId = null;
            }

            try
            {
                // Validate provided chavrisa id refers to an existing user BEFORE inserting
                if (user.ChavrisaId.HasValue)
                {
                    var referenced = await _context.Users.FindAsync(user.ChavrisaId.Value);
                    if (referenced == null)
                        return BadRequest(new { message = $"Chavrisa with id {user.ChavrisaId.Value} not found." });
                    if (referenced.UserId == user.UserId)
                        return BadRequest(new { message = "A user cannot be their own chavrisa." });
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // new user id available

                // reciprocal update
                if (user.ChavrisaId.HasValue && user.ChavrisaId.Value > 0 && user.ChavrisaId.Value != user.UserId)
                {
                    var other = await _context.Users.FindAsync(user.ChavrisaId.Value);
                    if (other != null)
                    {
                        other.ChavrisaId = user.UserId;
                        _context.Users.Update(other);
                        await _context.SaveChangesAsync();
                    }
                }

                // null navigation properties before serializing to avoid cycles
                user.Chavrisa = null;
                var otherForSerialization = user.ChavrisaId.HasValue ? await _context.Users.FindAsync(user.ChavrisaId.Value) : null;
                if (otherForSerialization != null) otherForSerialization.Chavrisa = null;

                return Ok();
            }
            catch (Exception ex)
            {
                // return error details to the client for debugging (remove in production)
                return StatusCode(500, new { message = ex.Message, inner = ex.InnerException?.Message, stack = ex.StackTrace });
            }
        }


        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound();

            // Use a transaction to keep DB consistent
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Clear chavrisa links that point to this user
                var dependents = await _context.Users
                    .Where(u => u.ChavrisaId == userId)
                    .ToListAsync();

                if (dependents.Any())
                {
                    foreach (var d in dependents)
                    {
                        d.ChavrisaId = null;
                        _context.Users.Update(d);
                    }
                    await _context.SaveChangesAsync();
                }

                // Now safe to remove the user
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return StatusCode(500, new { message = "Error deleting user", detail = ex.Message });
            }
        }
    }
}
