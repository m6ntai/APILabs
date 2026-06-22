using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCalculator.Models;

namespace WpfCalculator.NewFolder1
{
   
    public interface ICalculationRepository
    {
        void Add(Calculation calculation);
        List<Calculation> GetAll();
        Calculation GetById(int id);
    }

}
