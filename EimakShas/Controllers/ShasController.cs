using EimakShas.Data;
using Microsoft.AspNetCore.Mvc;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShasController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbContext;

        [HttpGet("masechtas")]
        public IActionResult GetMasechtas()
        {
            var masechtas = _context.Masechtas
                .ToList();
            return Ok(masechtas);
        }

        [HttpGet("dafim/{masechtaId}")]
        public IActionResult GetDafim(int masechtaId)
        {
            var dafim = _context.Dafim
                .Where(d => d.MasechtaId == masechtaId)
                .ToList();
            return Ok(dafim);
        }

    }
}
