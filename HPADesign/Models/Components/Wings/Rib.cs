using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Reactive.Bindings;
using HPADesign.IO.Components;
using HPADesign.IO;
using HPADesign.Models.Shape;
using System.Linq;
using HPADesign.Helpers;
using HPADesign.Models.Airfoils;

namespace HPADesign.Models.Components.Wings
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
        public ReactiveProperty<double> Chord { get; set; } = new ReactiveProperty<double>();

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

        /// <summary>
        /// リブキャップ
        /// </summary>
        public ReactiveProperty<RibCap> RibCap { get; set; } = new ReactiveProperty<RibCap>();

        private Rib() : base()
        {
            BindProperty(RibCap);
        }
        public Rib(WingSection parent) : this()
        {
            if (parent != null)
            {
                parent.Ribs.Add(this);
            }
        }
        public Rib(WingSection parent, int index) : this()
        {
            if (parent != null)
            {
                parent.Ribs.Insert(index, this);
            }
        }

        private List<Pos> HalfShape(List<Pos> shape, PriorityQueue<double,Stringer> stringers, double plankPos, AirfoilSide side)
        {
            var result = new List<Pos>();

            WingSection partWing = Parent.Value as WingSection;
            Plank plank = partWing.Plank.Value as Plank;
            TrainingEdge trainingEdge = partWing.TrainingEdge.Value as TrainingEdge;
            RibCap ribCap = RibCap.Value as RibCap;

            double endPos = Chord.Value - trainingEdge.TrainlingEdgeLength.Value;

            result.Add(new Pos(endPos, 0));
            //PlankとTrainingEdgeの位置をマークする
            for (int i = 0; i < shape.Count - 1; i++)
            {
                if (shape[i].x > endPos
                    && endPos > shape[i + 1].x)
                {
                    Pos mark = new Pos(
                        endPos,
                        Cal.Lerp(shape[i], shape[i + 1], endPos)
                        );
                    result.Add(mark);
                }
                    if (shape[i].x > plankPos * Chord.Value
                    && plankPos * Chord.Value > shape[i + 1].x)
                {
                    Pos mark = new Pos(
                        plankPos * Chord.Value,
                        Cal.Lerp(shape[i], shape[i + 1], plankPos * Chord.Value)
                        );
                    shape.Insert(i + 1, mark);
                    break;
                }
            }

            double pos_x = endPos;
            double reverse = (side == AirfoilSide.Upper) ? 1 : -1;
            for (int i = 1; i < shape.Count - 1; i++)
            {
                if (pos_x < shape[i].x)
                {
                    continue;
                }

                if (pos_x >= plankPos * Chord.Value)
                {
                    Pos mark = shape[i]
                        + reverse * ribCap.RibCapThin.Value * shape[i - 1].NormalUnitVector(shape[i + 1]);
                    result.Add(mark);
                }
                if (pos_x <= plankPos * Chord.Value)
                {
                    Pos mark = shape[i]
                        + reverse * plank.PlankThin.Value * shape[i - 1].NormalUnitVector(shape[i + 1]);
                    result.Add(mark);
                }

                pos_x = shape[i].x;
                while (stringers.Count() > 0 && stringers.PeekKey() > shape[i + 1].x)
                {
                    Stringer stringer = stringers.Take();
                    double initDepth = (stringer.StringerPos.Value <= plankPos) ?
                        plank.PlankThin.Value : ribCap.RibCapThin.Value;

                    //1点目、ストリンガーのx座標まで移動してマークする
                    Pos mark = new Pos(
                        stringer.StringerPos.Value * Chord.Value,
                        Cal.Lerp(shape[i], shape[i + 1], stringer.StringerPos.Value * Chord.Value),
                        0
                        ) + reverse * initDepth * shape[i].NormalUnitVector(shape[i + 1]);

                    //2点目
                    Pos digDepth = mark
                        + reverse * stringer.StringerHeight.Value * shape[i].NormalUnitVector(shape[i + 1]);

                    Pos forward = digDepth
                        + stringer.StringerWidth.Value * shape[i].DirectionUnitVector(shape[i + 1]);

                    Pos merge = forward
                        - reverse * stringer.StringerHeight.Value * shape[i].NormalUnitVector(shape[i + 1]);
                    pos_x = (merge - reverse * initDepth * shape[i].NormalUnitVector(shape[i + 1])).x;

                    result.Add(mark);
                    result.Add(digDepth);
                    result.Add(forward);
                    result.Add(merge);
                }
            }

            result.Add(new Pos(plank.PlankThin.Value, 0));
            return result;
        }

        public List<IPrintableElement> Shapes {
            get
            {
                var result = new List<IPrintableElement>();           
                
                Coordinate ribShape = new Coordinate();
                ribShape.Points = new List<Pos>();

                Coordinate baseShape = new Coordinate();
                baseShape.Points = Airfoil.Coordinate.Points.Select(u => Chord.Value * u).ToList();
                
                //imos法で掘り下げていく

                WingSection partWing = Parent.Value as WingSection;
                Plank plank = partWing.Plank.Value as Plank;
                TrainingEdge trainingEdge = partWing.TrainingEdge.Value as TrainingEdge;
                List<Stringer> stringers = partWing.Stringers.Select(x => (Stringer)x).ToList();
                
                
                var upperBaseShape = Airfoil.Coordinate.Upper.Data
                    .Select(u => Chord.Value * u)
                    .ToList();
                //upperBaseShape.OrderByDescending(u => u.x);
                var downerBaseShape = Airfoil.Coordinate.Downer.Data
                    .Select(u => Chord.Value * u)
                    .ToList();
                downerBaseShape.Reverse();

                var upperParts = new PriorityQueue<double, Stringer>(false);
                var downerParts = new PriorityQueue<double, Stringer>(false);

                stringers.Where(x => x.AirfoilSide.Value == AirfoilSide.Upper)
                    .ToList()
                    .ForEach(x => upperParts.Add(Chord.Value * x.StringerPos.Value, x));
                stringers.Where(x => x.AirfoilSide.Value == AirfoilSide.Downer)
                    .ToList()
                    .ForEach(x => downerParts.Add(Chord.Value * x.StringerPos.Value, x));

                var upperShape = HalfShape(upperBaseShape, upperParts, plank.PlankUpperPos.Value, AirfoilSide.Upper);
                var downerShape = HalfShape(downerBaseShape, downerParts, plank.PlankDownerPos.Value, AirfoilSide.Downer);

                downerShape.Reverse();
                ribShape.Points = upperShape;
                ribShape.Points.AddRange(downerShape);
                result.Add(ribShape);
                
                return result;
            }
        }
    }
}
