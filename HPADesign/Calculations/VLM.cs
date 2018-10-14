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
                        r[k] = new Pos(Elements[j].pr.Entry[k] - Elements[j].pl.Entry[k],
                            Elements[i].pcp.Entry[k]-Elements[j].pl.Entry[k],
                            Elements[i].pcp.Entry[k]-Elements[j].pr.Entry[k]);
                    }
                    double qpsirel = 0;

                    //Opt.TODO
                    for(int k=0; k<3; k++)
                    {
                        qpsirel += r[0].Entry[k] * (r[1].UnitVector.Entry[k] - r[2].UnitVector.Entry[k]);
                    }

                    Pos phidrel = Pos.CrossProduct(r[0],r[1]);
                    Pos vabrel = (Pos)(qpsirel * phidrel);
                    //後曳渦(左)
                    Pos vae = new Pos();
                    
                    vae.y = r[1].Entry[2] /
                        (new Pos(0, r[1].Entry[1], r[1].Entry[2]).Magnitude) *
                        (1 + r[1].UnitVector.Entry[0]);

                    vae.z = -r[1].Entry[1] /
                        new Pos(0, r[1].Entry[1], r[1].Entry[2]).Magnitude *
                        (1 + r[1].UnitVector.Entry[0]);

                    //後曳渦(右)
                    Pos veb = new Pos();
                    veb.y=r[2].Entry[2] /
                        (new Pos(0, r[2].Entry[1], r[2].Entry[2]).Magnitude) *
                        (1 + r[2].UnitVector.Entry[0]);

                    veb.z = -r[2].Entry[1] /
                        new Pos(0, r[1].Entry[1], r[1].Entry[2]).Magnitude *
                        (1 + r[1].UnitVector.Entry[0]);

                    Pos vij = (Pos)((vabrel + vae + veb) / (4.0 * Math.PI));

                    Matrix Phi = new Matrix(Cal.Cos(Elements[i].Phi))
                    //qijzを計算

                }
            }
        }
    }
}
