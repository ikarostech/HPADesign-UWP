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
        

        Airfoil airfoil;
        Pos lf { get; set; }
        Pos lr { get; set; }
        Pos rf { get; set; }
        Pos rr { get; set; }

        Pos pl { get; set; }
        Pos pr { get; set; }

        Pos pcp { get; set; }

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
        List<List<WingElement>> Elements;

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
            
        }
    }
}
