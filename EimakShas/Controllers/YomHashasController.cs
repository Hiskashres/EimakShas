using EimakShas.Data;
using EimakShas.DTOs;
using EimakShas.Services;
using Microsoft.AspNetCore.Mvc;

namespace EimakShas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YomHashasController(ApplicationDbContext _context) : ControllerBase
    {
        //private readonly ApplicationDbContext _context = dbContext;
        private YomHashasService yomHashasService = new (_context);


        [HttpPost("AssignDafim")]
        public IActionResult AssignDafimToYomHashas(int[] dafimIds)
        {
            yomHashasService.AddDafimToYomHashas(dafimIds);
            return Ok();
        }

        [HttpPost("completeDaf")]
        public IActionResult MarkDafAsCompleted(int dafId)
        {
            yomHashasService.MarkDafAsCompleted(dafId);
            return Ok();
        }

        [HttpPost("setGoals")]
        public IActionResult SetGoals(int firstGoal, int bonusGoal, TimeOnly endTime)
        {
            yomHashasService.SetGoals(firstGoal, bonusGoal, endTime);
            return Ok();
        }

        [HttpGet("getYomHashasInfo")]
        public IActionResult GetYomHashasInfo() 
        { 
            return Ok(yomHashasService.GetYomHashasInfo());
        }
    }
}
