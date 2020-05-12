using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Reactive.Bindings;

namespace HPADesign.Models.Component
{
    public class Rib : Component
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

        //桁穴位置
        public ReactiveProperty<Pos> SparHolePos { get; set; }

        // public Rib(Project project) : base(project) { }
    }
    public class Stringer : Component
    {
        //ストリンガー位置(mm)
        public ReactiveProperty<double> StringerPos { get; set; }

        //ストリンガー横厚(mm)
        public ReactiveProperty<double> StringerWidth { get; set; }

        //ストリンガー縦厚(mm)
        public ReactiveProperty<double> StringerHeight { get; set; }
    }
    public class Plank : Component
    {
        public ReactiveProperty<double> PlankThin { get; set; }

        //プランク取付位置
        public ReactiveProperty<Pos> PlankPos { get; set; }
    }

    public class TrainingEdge : Component
    {
        //後縁キャップ厚(mm)
        public ReactiveProperty<double> TrailingEdgeLidThin { get; set; }

        //後縁切り取り位置(mm)
        public ReactiveProperty<double> TrainlingEdgeCutPos { get; set; }
    }

}
