using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using Prism.Mvvm;

namespace HPADesign.Models
{
    public class Rib : BindableBase
    {
        //リブの名前
        public double Name { get; set; }

        private double chord;
        //翼弦長(mm)
        public double Chord
        {
            get
            {
                return chord;
            }
            set
            {
                chord = value;
                RaisePropertyChanged(nameof(Chord));
                
                
            }
        }

        //ねじり角(deg)
        public double Twist { get; set; }
        //たわみ角(deg)
        public double Dihedral { get; set; }

        //プランク厚(mm)
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


        public IAirfoil Foil { get; set; }

        public Rib() { return; }
        
        public Rib(double chord)
        {
            Chord = chord;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RibDXF()
        {
            //ファイルを開く
            var sw = new StreamWriter(new FileStream(Name + ".dxf", FileMode.Open), Encoding.ASCII);

            var OuterFoil = new List<double[]>();


            //まず拡大
            for (int i = 0; i < Foil.Coordinate.Coordinate321.Count; i++)
            {
                var o_point = new double[2] { Foil.Coordinate321[i].x * Chord, Foil.Coordinate321[i].y * Chord };

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
