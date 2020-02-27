using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Calculations
{
    public class XFoil : Solver
    {
        public double Re { get; set; }
        public double Alpha { get; set; }
        public double Mach { get; set; } = 0;
        public double NCrit { get; set; }
        public Pos XStrip { get; set; }

        public XFoil()
        {

        }

        public void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
