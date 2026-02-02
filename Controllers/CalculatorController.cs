using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
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
        public class CalcRequest
        {
            public double A { get; set; }
            public double B { get; set; }
        }
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalcRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is null");
            }

            double result = Math.Pow(request.A, request.B); // Вариант 4
            var calculation = new Calculation
            {
                A = request.A,
                B = request.B,
                Result = result,
                CreatedAt = DateTime.Now
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<Calculation>> GetAll()
        {
            return _context.Calculations.ToList();
        }
    }
}

