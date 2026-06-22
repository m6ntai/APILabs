using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;

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
        public IActionResult Calculate(CalcRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            double result = 0;

     
            result = request.A + request.B;

            
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
        public IActionResult GetAll()
        {
            var history = _context.Calculations.ToList();
            return Ok(history);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var calculation = _context.Calculations.FirstOrDefault(c => c.Id == id);
            if (calculation == null)
            {
                return NotFound();
            }
            return Ok(calculation);
        }

        [HttpGet("add/{a}/{b}")]
        public IActionResult AddFromUrl(double a, double b)
        {
            
            double result = a + b; 

            return Ok(result);
        }
    }
}