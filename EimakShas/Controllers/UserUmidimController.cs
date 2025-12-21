using EimakShas.Data;
using EimakShas.Models.Requests;
using EimakShas.Services;
using Microsoft.AspNetCore.Mvc;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserUmidimController (ApplicationDbContext _context) : ControllerBase
    {
        UserUmidService userUmidService = new (_context);

        [HttpPost("completeDafim")]
        public IActionResult MarkUserDafimAsComplete([FromBody] CompleteDafimRequest request)
        {
            userUmidService.MarkUserDafAsComplete(request.DafimIds, request.UserId);

            return Ok();
        }
    }
}
