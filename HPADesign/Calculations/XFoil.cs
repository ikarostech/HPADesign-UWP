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

        public double C_L { get; set; }
        public double C_M { get; set; }

        public Airfoil CurrentFoil { get; set; }

        private Vector qvis { get; set; }
        private double qinf { get; set; }
        public XFoil()
        {

        }
        private void qvisSolve()
        {
            for(int i=0; i<2; i++)
            {
                for(int j=2; j<=nbl[i]; j++)
            }
        }
        public void CLSolve()
        {
            double beta = 0;
            double bfac = 0;

            C_L = 0;
            C_M = 0;

            Vector cginc = qvis.Map(x => { return 1.0 - Math.Pow(x/qinf,2); });
            Vector cpg = cginc.Map(x => { return x / (beta + bfac * x); });

            for(int i=0; i<CurrentFoil.Coordinate321.Count-1; i++)
            {
                
                Pos diff = CurrentFoil.Coordinate321[i + 1] - CurrentFoil.Coordinate321[i];
                Pos arg = new Pos(Cal.Cos(Alpha), Cal.Sin(Alpha));
                Pos ds = diff.Rotation2DVector(-Alpha);
                double cpgAvg = (cpg.Entry[i + 1] - cpg.Entry[i]) / 2;

                C_L += ds.x * cpgAvg;
                //C_M -= Pos.InnerProduct(ds, )
            }
            
        }
        public void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
