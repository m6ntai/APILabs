using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace CalculatorAPI.Data
{

    public class CalcRequest
    {
        public double A { get; set; }
        public double B { get; set; }
    }
}