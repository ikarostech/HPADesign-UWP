using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Reactive.Bindings;
using HPADesign.IO.Component;
using HPADesign.IO;
using HPADesign.Models.Shape;
using System.Linq;

namespace HPADesign.Models.Component
{
    public class Rib : Component, IPrintable
    {
        /// <summary>
        /// 翼型
        /// </summary>
        public Airfoil Airfoil { get; set; } = new Airfoil();

        /// <summary>
        /// 翼弦長
        /// </summary>
        public ReactiveProperty<int> Chord { get; set; } = new ReactiveProperty<int>();

        /// <summary>
        /// ねじり角
        /// </summary>
        public ReactiveProperty<double> Twist { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// たわみ角
        /// </summary>
        public ReactiveProperty<double> Dihedral { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// 桁穴位置
        /// </summary>
        public ReactiveProperty<Pos> SparHolePos { get; set; }

        public List<IPrintableElement> Shapes {
            get
            {
                var result = new List<IPrintableElement>();
                Coordinate baseShape = Airfoil.Coordinate;
                baseShape.Points = baseShape.Points.Select(x => Chord.Value * x).ToList();

                //imos法で掘り下げていく

                PartWing partWing = Parent.Value as PartWing;
                Plank plank = partWing.Plank;
                ReactiveCollection<Stringer> stringers = partWing.Stringers;

                var upper_imos = new List<KeyValuePair<double, double>>();
                var downer_imos = new List<KeyValuePair<double, double>>();
                return result;
            }
        }

        // public Rib(Project project) : base(project) { }
    }
    public class Stringer : Component
    {
        /// <summary>
        /// ストリンガー取付側
        /// </summary>
        public ReactiveProperty<AirfoilSide> AirfoilSide { get; set; }

        /// <summary>
        /// ストリンガー取付位置(%)
        /// </summary>
        public ReactiveProperty<double> StringerPos { get; set; }

        /// <summary>
        /// ストリンガー横厚(mm)
        /// </summary>
        public ReactiveProperty<double> StringerWidth { get; set; }

        /// <summary>
        /// ストリンガー縦厚(mm)
        /// </summary>
        public ReactiveProperty<double> StringerHeight { get; set; }
    }
    public class Plank : Component
    {
        public ReactiveProperty<double> PlankThin { get; set; }

        /// <summary>
        /// プランク上側取付位置(%)
        /// </summary>
        public ReactiveProperty<double> PlankUpperPos { get; set; }

        /// <summary>
        /// プランク下側取付位置(%)
        /// </summary>
        public ReactiveProperty<double> PlankDownerPos { get; set; }
    }

    public class TrainingEdge : Component
    {
        //後縁キャップ厚(mm)
        public ReactiveProperty<double> TrailingEdgeLidThin { get; set; }

        //後縁切り取り位置(mm)
        public ReactiveProperty<double> TrainlingEdgeCutPos { get; set; }
    }

}
