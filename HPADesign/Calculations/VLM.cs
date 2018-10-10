using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;

namespace HPADesign.Calculations
{
    /// <summary>
    /// VLM法における翼素
    /// </summary>
    public class WingElement
    {
        

        public Airfoil airfoil;
        public Pos lf { get; set; }
        public Pos lr { get; set; }
        public Pos rf { get; set; }
        public Pos rr { get; set; }

        public Pos pl { get; set; }
        public Pos pr { get; set; }

        public Pos pcp { get; set; }

        double phi { get; set; }
        double seta { get; set; }

        double ds { get; set; }
        double dl { get; set; }
        double dc { get; set; }

        double dzdx { get; set; }
        double ganma { get; set; }
        double Downwash { get; set; }
        double Cp { get; set; }
    }
    public class VLM : Solver
    {
        List<WingElement> Elements;

        double Alpha { get; set; }

        //VLMコンディションはこちらに
        public double Dihedral { get; set; }
        public double Twist { get; set; }
        public int xnum { get; set; }
        public int ynum { get; set; }
        public double xoffset { get; set; }
        public double ypos { get; set; }
        public double chord { get; set; }

        /// <summary>
        /// wingからElementsを作成
        /// </summary>
        /// <param name="wing"></param>
        public VLM(Wing wing)
        {
            //パネルデータ生成
            Pos rf, rr, tf, tr;

            double yk, ck, osk, dk, ak, xnumk, ynumk, dam0, dam1, dam2, ykp1, ckp1, oskp1, dkp1, akp1, xnumkp1, ynumkp1;

            
        }
        public void Solve()
        {
            //ビオサバールの影響係数を計算
            Matrix Qizj = new Matrix(Elements.Count, Elements.Count);

            for(int i=0; i<Elements.Count; i++)
            {
                for(int j=0; j<Elements.Count; j++)
                {
                    Pos[] r = new Pos[3];

                    for(int k=0; k<3; k++)
                    {
                        r[k] = new Pos(Elements[j].pr.Entity[k] - Elements[j].pl.Entity[k],
                            Elements[i].pcp.Entity[k]-Elements[j].pl.Entity[k],
                            Elements[i].pcp.Entity[k]-Elements[j].pr.Entity[k]);
                    }
                    double qpsirel = 0;

                    //Opt.TODO
                    for(int k=0; k<3; k++)
                    {
                        qpsirel += r[0].Entity[k] * (r[1].UnitVector.Entity[k] - r[2].UnitVector.Entity[k]);
                    }

                    Pos phidrel = Pos.CrossProduct(r[0],r[1]);

                    //後曳渦(左)
                    double vaexrel = 0.0;
                    double vaeyrel = r[1].Entity[2] /
                        (new Pos(0, r[1].Entity[1], r[1].Entity[2]).Magnitude) *
                        (1 + r[1].UnitVector.Entity[0]);
                    //後曳渦(右)
                }
            }
        }
    }
}
