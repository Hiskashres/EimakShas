using EimakShas.Data;
using Microsoft.AspNetCore.Mvc;
using EimakShas.Services;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YomHashasController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbContext;

        [HttpPost("AssignDafim")]
        public IActionResult AssignDafimToYomHashas(int masechtaId, int[] dafimIds)
        {
            var service = new YomHashasService(_context);
            service.AddDafimToYomHashas(dafimIds);

            return Ok();
        }

    }
}
