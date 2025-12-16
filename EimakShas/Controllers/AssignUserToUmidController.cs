using EimakShas.Data;
using EimakShas.Models;
using EimakShas.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignUserToUmidController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbContext;

        [HttpPost("AssignDafim")]
        public async Task<IActionResult> AssignDafim([FromBody] AssignDafimRequest request)
        {
            var umidim = _context.Umidim
                .Where(u => request.DafimIds.Contains(u.DafId))
                .ToList();

            var newUmidim = new List<UserUmid>();

            foreach (var u in umidim)
            {
                bool alreadyExists = _context.UserUmidim
                    .Any(x => x.UserId == request.UserId && x.UmidId == u.UmidId);

                int addedUmidimAmount = 0;
                int addedDafimAmount = 0;
                if (!alreadyExists)
                {
                    newUmidim.Add(new UserUmid { UserId = request.UserId, UmidId = u.UmidId });

                    //var user = _context.Users
                    //    .FirstOrDefault(user => user.UserId == request.UserId);
                    //user.DafimAmount++;

                    addedUmidimAmount++;
                    //if (addedUmidimAmount == 2) addedUmidimAmount = 0; addedDafimAmount++;
                }
                var user = _context.Users
                    .FirstOrDefault(user => user.UserId == request.UserId);
                addedDafimAmount = addedUmidimAmount * 2;
                user.DafimAmount = user.DafimAmount + addedDafimAmount;
            }

            _context.UserUmidim.AddRange(newUmidim);
            _context.SaveChanges();

            return Ok(new { Added = newUmidim.Count });
        }
    }
}
