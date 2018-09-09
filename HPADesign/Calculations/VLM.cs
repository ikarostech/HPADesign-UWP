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
    public class VLM
    {
        
        public VLM()
        {

        }
    }
}
