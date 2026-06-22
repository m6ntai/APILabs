
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalculatorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalcRequest request) 
        {
            double result = request.A + request.B;

            var calculation = new CalcRegues
            {
                A = request.A,
                B = request.B,
                Result = result,
                CreatedAt = DateTime.Now
            };

           
            _context.Calculations.Add(calculation);
            await _context.SaveChangesAsync(); 

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory() 
        {
           
            var history = await _context.Calculations.ToListAsync();
            return Ok(history);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Web API работает!");
        }

       
        [HttpGet("testdb")]
        public async Task<IActionResult> TestDb()
        {
            var count = await _context.Calculations.CountAsync();
            return Ok($"В БД {count} записей");
        }
    }

    internal class CalcRegues : CalcReques
    {
        public double A { get; set; }
        public double B { get; set; }
        public double Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CalcRequest
    {
        public double A { get; set; }
        public double B { get; set; }
    }
}