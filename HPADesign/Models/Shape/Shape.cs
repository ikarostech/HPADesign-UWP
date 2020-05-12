using HPADesign.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    public abstract class Shape
    {
        public abstract Pos GravityCenter { get; }
    }
    public abstract class Shape2D : Shape , IPrintableElement
    {
        public abstract double Area { get; }

        /// <summary>
        /// DXFのElementを出力します
        /// </summary>
        public abstract string Print();
    }
    public abstract class Shape3D : Shape2D
    {
        /// <summary>
        /// 体積（いる？）
        /// </summary>
        public abstract double Volume { get; set; }
    }

    public class Circle : Shape2D
    {
        public override double Area
        {
            get
            {
                return Math.Pow(Area,2)* Math.PI;
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
            return null;
        }
    }


}
