using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Helpers;
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



        /// <summary>
        /// Unity Free Stream Speed
        /// </summary>
        public double Qinf { get; set; }

        public XFoil(Airfoil airfoil)
        {
            CurrentFoil = airfoil;
        }
        public void Solve()
        {
            throw new NotImplementedException();
        }
    }

    public class XFoilAnalyzer
    {
        public XFoil Parent { get; }
        private int N { get { return Parent.CurrentFoil.Coordinate321.Count; } }
        public double Alpha { get; set; }
        private double _mach;
        public double Mach
        {
            get { return _mach; }
            set
            {
                _mach = value;

            }
        }
        public double Reynolds { get; set; }

        public double NCrit { get; set; }
        public double XtrTop { get; set; }
        public double XtrBot { get; set; }
        public int reType { get; set; }
        public int maType { get; set; }
        public bool vBiscous { get; set; }

        public double Qinf { get; set; }
        public double Minf { get; set; }



        /// <summary>
        /// 
        /// </summary>
        private List<Pos> gammaU
        {
            get
            {
                var result = new List<Pos>(Parent.CurrentFoil.Coordinate321.Count + 1);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = Qinf * Parent.CurrentFoil.Coordinate321[i].Inverse();
                }
                result[result.Count] = new Pos(0, 0);
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private List<double> gamma
        {
            get
            {
                return gammaU
                    .Select(x => Vector.InnerProduct(Pos.Rotation(Alpha), x))
                    .ToList();
            }
        }
        private double Gamma { get { return 1.4; } }

        private double sigTE { get { return (gamma[0] - gamma[gamma.Count - 1]) / 2; } }
        private double gamTE { get { return 0; } }

        private double tklam { get { return Math.Pow(Minf / Math.Sqrt(1 - Minf * Minf), 2); } }

        public Matrix A
        {
            get
            {
                int N = Parent.CurrentFoil.Coordinate321.Count;
                var result = new Matrix(N, N);
                Vector dzdg = new Vector(N);
                Vector dzdn = new Vector(N);
                Vector dqdg = new Vector(N);
                Vector dzdm = new Vector(N);
                Vector dqdm = new Vector(N);

                List<Pos> r = Parent.CurrentFoil.Coordinate321
                    .Select(x => x - new Pos(1, 0))
                    .ToList();



                return result;
            }
        }

        public XFoilAnalyzer(double alpha)
        {
            Alpha = alpha;
            Qinf = 1.0;
        }

        // nx,ny
        List<Pos> Sp { get; set; }

        //
        List<double> Psi { get; set; }
        public void psilin()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Parent.CurrentFoil.Coordinate321[j].Equals(Parent.CurrentFoil.Coordinate321[(j + 1) % N]))
                    {
                        continue;
                    }

                    //sgn = 1.0;


                }
            }
            //scs = 1.0, sds = 0.0
        }

        /// <summary>
        /// 超音波の影響を調べる（実装無し）
        /// </summary>
        private void mrcl()
        {
            throw new NotImplementedException();
        }



    }

    public class C_Analyzer
    {
        public Airfoil CurrentFoil { get; set; }
        public double Alpha { get; set; }

        public double Qinf { get; set; }
        public double Minf { get; set; }

        public double C_L { get; set; }
        public double C_M { get; set; }

        private Vector qvis { get; set; }

        public C_Analyzer(Airfoil airfoil, double alpha, Vector Qvis, double Qinf = 1.0)
        {
            CurrentFoil = airfoil;
            Alpha = alpha;
            this.Qinf = Qinf;
            this.qvis = qvis;
        }
        public void CLSolve()
        {
            double beta = 0;
            double bfac = 0;

            C_L = 0;
            C_M = 0;

            Vector cginc = qvis.Map(x => { return 1.0 - Math.Pow(x / Qinf, 2); });
            Vector cpg = cginc.Map(x => { return x / (beta + bfac * x); });

            for (int i = 0; i < CurrentFoil.Coordinate321.Count - 1; i++)
            {
                Pos diff = CurrentFoil.Coordinate321[i + 1] - CurrentFoil.Coordinate321[i];
                Pos ds = diff.Rotation2DVector(-Alpha);
                double cpgAvg = (cpg.Entry[i + 1] - cpg.Entry[i]) / 2;

                C_L += ds.x * cpgAvg;
                //C_M -= Pos.InnerProduct(ds, )
            }

        }
        List<double> q { get; set; }
        List<double> C_P { get; set; }
        /// <summary>
        /// COmpressible CP from Speed
        /// </summary>
        public void CPSolve()
        {
            double beta = (1 - Minf * Minf);
            double bfac = Minf * Minf / (1 + beta) / 2;

            C_P = q
                .Select(x => 1.0 - (x / Qinf) * (x / Qinf))
                .Select(x => x / (beta + bfac * x))
                .ToList();
            if (C_P.Any(x => x < 0))
            {
                //throw new Exception();
            }
        }

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
                var result = new List<Pos>(Airfoil.N);
                return result;
            }
         }
        private List<double> gamma { get { return gammaU.Select(x=>Vector.InnerProduct(x,Pos.Rotation(Alpha))).ToList(); } }
        private double _gamma = 1.4;
        public List<Pos> QU { get { throw new NotImplementedException(); } }
        private List<double> Q { get { return QU.Select(x => Vector.InnerProduct(x, Pos.Rotation(Alpha))).ToList(); } }
    
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

            //clcalc xref = 0.25 yref = 0
            double cl = 0;
            double cm = 0;
            double cdp = 0;
            double xcp = 0;

            double beta = 0;
            double bfac = 0;

            Vector cginc = new Vector(gamma.Select(x => { return 1.0 - Math.Pow(x / Qinf, 2); }).ToList());
            Vector cpg = cginc.Map(x => { return x / (beta + bfac * x); });
            Pos Ref = new Pos(0.25, 0);
            for (int i = 0; i < Airfoil.N - 1; i++)
            {
                Pos diff = Foil.Coordinate321[i + 1] - Foil.Coordinate321[i];
                Pos avg = Foil.Coordinate321[i + 1] + Foil.Coordinate321[i] / 2;
                Pos ds = diff.Rotation2DVector(-Alpha);
                double cpgDiff = (cpg.Entry[i + 1] - cpg.Entry[i]);
                double cpgAvg = (cpg.Entry[i + 1] + cpg.Entry[i]) / 2;

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

            bool bConv = false; //計算が収束済みかを定義
            //マッハ数による影響を考える場合はニュートン法でCLの収束を計算する必要がある。
            /*
            for(int i=0; i<20; i++)
            {

            }
            */

            //cpcalc(n,qinv,qinf,minf,cpi)
            Vector cpi = new Vector(Airfoil.N);
        }

    }

}
