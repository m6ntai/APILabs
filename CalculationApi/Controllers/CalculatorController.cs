using Microsoft.AspNetCore.Mvc;
using CalculationApi.Models;
using CalculationApi.DTOs;

using Microsoft.EntityFrameworkCore; 


namespace CalculationApi.Controllers
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

        public class CalcRequest
        {
            public double A { get; set; }
            public double B { get; set; }
        }


        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalcRequest request)
        {
            if (request == null)
                return BadRequest("Нет данных");

       
            double result = request.A * request.B;

       
            var calculation = new Calculation
            {
                A = request.A,
                B = request.B,
                Result = result,
                CreatedAt = DateTime.Now
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new { Result = result });
        }

      
        [HttpGet]
        public List<Calculation> GetAll()
        {
            return _context.Calculations.ToList();
        }

       
        [HttpGet("{id}")]
        public ActionResult<Calculation> GetById(int id)
        {
            var calculation = _context.Calculations.FirstOrDefault(c => c.Id == id);

            if (calculation == null)
            {
                return NotFound($"Запись с ID {id} не найдена");
            }

            return calculation;
        }

      
        [HttpGet("add/{a}/{b}")]
        public double AddFromUrl(double a, double b)
        {
            
            return a * b;
        }

      
        [HttpGet("multiply/{a}/{b}")]
        public double MultiplyFromUrl(double a, double b)
        {
            return a * b;
        }
        [HttpPost("calculate-body")]
        public IActionResult CalculateFromBody([FromBody] CalcRequest request)
        {
            if (request == null)
                return BadRequest("Нет данных");

            double result = request.A * request.B;

            var calculation = new Calculation
            {
                A = request.A,
                B = request.B,
                Result = result,
                CreatedAt = DateTime.Now
            };
            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new { A = request.A, B = request.B, Result = result });
        }

      
        [HttpGet("calc/{a}/{b}")]
        public IActionResult CalculateFromRoute(double a, double b)
        {
            double result = a * b;

            var calculation = new Calculation
            {
                A = a,
                B = b,
                Result = result,
                CreatedAt = DateTime.Now
            };
            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new { A = a, B = b, Result = result });
        }

       
        [HttpGet("query")]
        public IActionResult CalculateFromQuery(double a, double b)
        {
            double result = a * b;

            var calculation = new Calculation
            {
                A = a,
                B = b,
                Result = result,
                CreatedAt = DateTime.Now
            };
            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new { A = a, B = b, Result = result });
        }

     
        [HttpGet("dto/{id}")]
        public ActionResult<CalculationDto> GetDto(int id)
        {
            var calc = _context.Calculations.FirstOrDefault(x => x.Id == id);

            if (calc == null)
                return NotFound($"Запись с ID {id} не найдена");

            var dto = new CalculationDto
            {
                A = calc.A,
                B = calc.B,
                Result = calc.Result
            };

            return Ok(dto);
        }

     
        [HttpGet("list")]
        public IActionResult GetList()
        {
            // Проекция в DTO прямо в запросе к БД
            var list = _context.Calculations
                .Select(c => new CalculationDto
                {
                    A = c.A,
                    B = c.B,
                    Result = c.Result
                })
                .ToList();

            return Ok(list);
        }

       
        [HttpGet("filter")]
        public IActionResult FilterByResult([FromQuery] double? minResult, [FromQuery] double? maxResult)
        {
            var query = _context.Calculations.AsQueryable();

            if (minResult.HasValue)
                query = query.Where(c => c.Result >= minResult.Value);

            if (maxResult.HasValue)
                query = query.Where(c => c.Result <= maxResult.Value);

            var results = query
                .Select(c => new CalculationDto
                {
                    A = c.A,
                    B = c.B,
                    Result = c.Result
                })
                .ToList();

            return Ok(results);
        }

        [HttpPost] 
        public IActionResult Create([FromBody] CreateCalculationDto dto)
        {
            if (dto == null)
                return BadRequest("Данные не предоставлены");

            double result = dto.A * dto.B;

            var calculation = new Calculation
            {
                A = dto.A,
                B = dto.B,
                Result = result,
                CreatedAt = DateTime.Now
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

          
            return CreatedAtAction(
                nameof(GetById),           
                new { id = calculation.Id }, 
                calculation                
            );
        }

    }
}