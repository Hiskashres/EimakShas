using EimakShas.Data;
using EimakShas.Models;
using EimakShas.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignUserToUmidController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbContext;

        [HttpPost("AssignDafim")]
        public IActionResult AssignDafim([FromBody] AssignDafimRequest request)
        {
            var umidim = _context.Umidim
                .Where(u => request.DafimIds.Contains(u.DafId))
                .ToList();

            var newUmidim = new List<UserUmid>();

            foreach (var u in umidim)
            {
                bool alreadyExists = _context.UserUmidim
                    .Any(x => x.UserId == request.UserId && x.UmidId == u.UmidId);

                if (!alreadyExists)
                    newUmidim.Add(new UserUmid { UserId = request.UserId, UmidId = u.UmidId });
            }

            _context.UserUmidim.AddRange(newUmidim);
            _context.SaveChanges();

            return Ok(new { Added = newUmidim.Count });
        }
    }
}
