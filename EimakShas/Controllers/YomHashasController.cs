using EimakShas.Data;
using EimakShas.DTOs;
using EimakShas.Models.Requests;
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


        [HttpPost("assignDafim")]
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
        public IActionResult SetGoals([FromBody] SetGoalsRequest request)
        {
            var endTime_TimeOnly = TimeOnly.Parse(request.EndTime);
            yomHashasService.SetGoals(request.MainGoal, request.BonusGoal, endTime_TimeOnly);
            return Ok();
        }

        [HttpGet("getYomHashasInfo")]
        public IActionResult GetYomHashasInfo() 
        { 
            return Ok(yomHashasService.GetYomHashasInfo());
        }
    }
}
