using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class CalculationDto
    {
        public double A { get; set; }
        public double B { get; set; }
        public double Result { get; set; }
    }
}