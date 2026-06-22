using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalculatorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateFromBody([FromBody] CalcRequest request)
        {
            double result = request.A + request.B;
            return Ok(new { request.A, request.B, Result = result });
        }

        [HttpGet("calc/{a}/{b}")]
        public IActionResult CalculateFromRoute(double a, double b)
        {
            double result = a + b;
            return Ok(new { A = a, B = b, Result = result });
        }

        [HttpGet("query")]
        public IActionResult CalculateFromQuery([FromQuery] double a, [FromQuery] double b)
        {
            double result = a + b;
            return Ok(new { A = a, B = b, Result = result });
        }

        [HttpGet("dto/{id}")]
        public ActionResult<CalculationDto> GetDto(int id)
        {
            var calc = _context.Calculations.FirstOrDefault(x => x.Id == id);
            if (calc == null) return NotFound();
            return Ok(new CalculationDto { A = calc.A, B = calc.B, Result = calc.Result });
        }

        [HttpGet("list")]
        public IActionResult GetList()
        {
            var list = _context.Calculations.Select(c => new CalculationDto { A = c.A, B = c.B, Result = c.Result }).ToList();
            return Ok(list);
        }
    }
}
