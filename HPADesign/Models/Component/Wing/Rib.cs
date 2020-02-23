using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Prism.Mvvm;

namespace HPADesign.Models.Component
{
    public class Rib : Component
    {
        /// <summary>
        /// 翼型
        /// </summary>
        public Airfoil Airfoil { get; set; }

        /// <summary>
        /// 翼弦長
        /// </summary>
        public int Chord { get; set; }

        
        /// <summary>
        /// リブ名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ねじり角
        /// </summary>
        public double Twist { get; set; }

        /// <summary>
        /// たわみ角
        /// </summary>
        public double Dihedral { get; set; }

        
        public double PlankThin { get; set; }

        //プランク取付位置
        public Tuple<double, double> PlankPos { get; set; }
        //ストリンガー位置(mm)
        public double StringerPos { get; set; }

        //ストリンガー横厚(mm)
        public double StringerWidth { get; set; }

        //ストリンガー縦厚(mm)
        public double StringerHeight { get; set; }

        //後縁キャップ厚(mm)
        public double TrailingEdgeLidThin { get; set; }

        //後縁切り取り位置(mm)
        public double TrainlingEdgeCutPos { get; set; }

        //桁穴位置
        public Pos Sparholepos { get; set; }

        // public Rib(Project project) : base(project) { }
    }
    public class WingRib : Rib
    {
        


        public Airfoil Airfoil { get; set; }

        private string airfoilname = string.Empty;
        //リブに使われている翼型の名前
        public string AirfoilName {
            get { return airfoilname; }
            set
            {

                airfoilname = value;
                //Parent.Update();
            }
        }

        //public WingRib(Project project) : base(project) { }

        

        public void RibDXF()
        {
            //ファイルを開く
            var sw = new StreamWriter(new FileStream(Name + ".dxf", FileMode.Open), Encoding.ASCII);

            var OuterFoil = new List<double[]>();


            //まず拡大
            for (int i = 0; i < Airfoil.Coordinate.Coordinate321.Count; i++)
            {
                var o_point = new double[2] { Airfoil.Coordinate321[i].x * Chord, Airfoil.Coordinate321[i].y * Chord };

                OuterFoil.Add(o_point);
            }
            //プランク部のところ


            //ヘッダーを書き込む
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("ENTITIES");
            sw.Dispose();
        }
    }

}
