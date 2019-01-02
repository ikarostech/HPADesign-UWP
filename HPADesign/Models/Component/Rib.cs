using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Prism.Mvvm;

namespace HPADesign.Models
{
    public class Rib : BindableBase
    {
        private PartWing Parent { get; set; }
        public Project project { get; set; }
        //リブの名前
        public string Name { get; set; }

        
        public int GlobalPosition
        {
            get
            {
                return localpos + Parent.StartPos;
            }

            set
            {
                localpos = value - Parent.StartPos;
                RaisePropertyChanged(nameof(LocalPosition));
                RaisePropertyChanged(nameof(GlobalPosition));

            }
        }

        private int localpos;
        public int LocalPosition
        {
            get
            {
                return localpos;
            }

            set
            {
                localpos = value;
                RaisePropertyChanged(nameof(LocalPosition));
                RaisePropertyChanged(nameof(GlobalPosition));
            }
        }

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


        public Airfoil Airfoil { get; set; }

        private string airfoilname = string.Empty;
        //リブに使われている翼型の名前
        public string AirfoilName {
            get { return airfoilname; }
            set
            {
                airfoilname = value;
                Parent.Update();
            }
        }

        public Rib(PartWing parent)
        {
            Parent = parent;
            project = Parent.project;
            return;
        }
        
        public Rib(PartWing parent,double chord,int localpos)
        {
            this.Parent = parent;
            project = Parent.project;
            Chord = chord;
            this.LocalPosition = localpos;
        }

        

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
