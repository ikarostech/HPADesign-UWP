using HPADesign.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Shape
{
    public class Circle : Shape2D
    {
        public override double Area
        {
            get
            {
                return Math.Pow(Area, 2) * Math.PI;
            }
        }
        public double Radius { get; set; }
        public Pos Center { get; set; }
        public override Pos GravityCenter { get; }
        /// <summary>
        /// 円のDXF情報を出力
        /// </summary>
        public override string Print()
        {
            var content = new List<KeyValuePair<int, string>>();
            content.Add(new KeyValuePair<int, string>(0, "CIRCLE"));
            content.Add(new KeyValuePair<int, string>(8, "0"));
            

            content.Add(new KeyValuePair<int, string>(10, Center.x.ToString()));
            content.Add(new KeyValuePair<int, string>(20, Center.y.ToString()));
            content.Add(new KeyValuePair<int, string>(30, Center.z.ToString()));
            content.Add(new KeyValuePair<int, string>(40, Radius.ToString()));

            return DXF.ParamToString(content);
        }
    }
}
