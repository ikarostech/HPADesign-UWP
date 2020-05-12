using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Helpers;
namespace HPADesign.Calculations
{
    public class XFoil 
    {
        
    }



    public class XFoil_Raw
    {
        Airfoil Foil { get; set; }

        //Constructor
        public double Re { get; set; }
        public double Alpha { get; set; }
        public double Mach { get; set; }
        public double NCrit { get; set; }
        public Vector Xstrip { get; set; }

        //setQInf
        public double Qinf { get; set; } = 1.0;

        //ggcalc
        private List<Pos> gammaU {
            get
            {
                var result = new List<Pos>(Foil.Coordinate.N);
                return result;
            }
         }
        private List<double> gamma { get { return gammaU.Select(x=>Vector.InnerProduct(x,Pos.Rotation(Alpha))).ToList(); } }
        private double _gamma = 1.4;
        public List<Pos> QU { get { throw new NotImplementedException(); } }
        private Vector Q { get { return new Vector(QU.Select(x => Vector.InnerProduct(x, Pos.Rotation(Alpha))).ToList()); } }
        private Vector Qvis { get { throw new Exception(); } }

        public XFoil_Raw(double Re,double alpha,double Mach,
            double NCrit, double XtrTop, double XtrBot, int reType, int maType, bool bViscous)
        {
            this.Re = Re;
            this.Alpha = alpha;
            this.Mach = Mach;
            this.NCrit = NCrit;
            Xstrip = new Vector(new double[2] { XtrTop, XtrBot });

            if(Mach>1E-5)
            {
                throw new Exception();
            }
        }
        
        public void specal()
        {
            //プロパティにより gamu qaijは自動計算
            //ggcalc

            //gam もプロパティで自動計算

            double psi = Vector.InnerProduct(gammaU[gammaU.Count - 1] , Pos.Rotation(Alpha));

            //tecalc()
            double sigte = (gamma[gamma.Count - 1] - gamma[0]) / 2;
            /*
            double gamte = 0;

            //mrcl() maType = 1, minf = minf1 = Mach, reType = 1
            double clm = 1;
            double minf = Mach;
            double m_cls = 0;

            double reinf = 0;
            double r_cls = 0;


            //comset() karman-tsien parameter
            double cpstar = -999;
            double qstar = 999;
            */
            //clcalc xref = 0.25 yref = 0
            double cl = 0;
            double cm = 0;
            double cdp = 0;
            double xcp = 0;

            double beta = 0;
            double bfac = 0;

            Vector cginc = new Vector(gamma.Select(x => { return 1.0 - Math.Pow(x / Qinf, 2); }).ToList());
            Vector cpg = new Vector(3);// = cginc.Map(x => { return x / (beta + bfac * x); });
            Pos Ref = new Pos(0.25, 0);
            for (int i = 0; i < Foil.Coordinate.N - 1; i++)
            {
                Pos diff = Foil.Coordinate.NormalPoints[i + 1] - Foil.Coordinate.NormalPoints[i];
                Pos avg = Foil.Coordinate.NormalPoints[i + 1] + Foil.Coordinate.NormalPoints[i] / 2;
                Pos ds = diff.Rotation2DVector(-Alpha);
                double cpgDiff = (cpg[i + 1] - cpg[i]);
                double cpgAvg = (cpg[i + 1] + cpg[i]) / 2;

                cl += ds.x * cpgDiff;
                cdp -= ds.y * cpgDiff;
                Pos a = cpgDiff * new Pos(
                    Vector.InnerProduct(avg - Ref, Pos.Rotation(Alpha)),
                    // -sina*x + cosa*y = cos(270-a)*x + sin(270-a)*y
                    Vector.InnerProduct(avg - Ref, Pos.Rotation(270-Alpha))
                    );

                
                cm -= Vector.InnerProduct(ds, (a + cpgAvg * ds / 12));

                xcp += cpgAvg * ds.x * avg.x;
                xcp /= cl;
                //C_M -= Pos.InnerProduct(ds, )
            }

            //bool bConv = false; //計算が収束済みかを定義
            //マッハ数による影響を考える場合はニュートン法でCLの収束を計算する必要がある。
            /*
            for(int i=0; i<20; i++)
            {

            }
            */

            //cpcalc(n,qinv,qinf,minf,cpi)
            Vector cpi = new Vector(Foil.Coordinate.N);
            //beta = (1-minf*minf) ^ 0.5 (前の処理で定義済み)

            //bfac

            cpi = new Vector(gamma.Select(x =>
            {
                double cpinc = 1 - Math.Pow(x / Qinf, 2);
                double den = beta + bfac * cpinc;
                return cpinc / den;
            }).ToList());
            Vector cpv = new Vector(Foil.Coordinate.N);
            /*
            cpv = new Vector(Qvis.Select(x =>
            {
                double cpvnc = 1 - Math.Pow(x / Qinf, 2);
                double den = beta + bfac * cpvnc;
                return cpvnc / den;
            }).ToList());
            */
        }

        //iterationを開始
        public void iterate()
        {
            for(int i=0; i<20; i++)
            {

            }
        }
        public void InitBL()
        {
            //set forced transition arc length position (使わない）

        }
        public void ViscousIter()
        {
            //setbl

        }
    }

}
