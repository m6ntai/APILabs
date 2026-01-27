using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost("calculate")]
        public double Calculate(CalcRequest request)
        {
            // Операция зависит от варианта:
            // 1: сложение, 2: умножение, 3: деление, 4: возведение в степень
            return request.A + request.B;
        }
    }
}