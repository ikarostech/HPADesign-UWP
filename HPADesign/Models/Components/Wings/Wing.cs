using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections.Generic;
using HPADesign.Models.Components;
using Reactive.Bindings;

namespace HPADesign.Models.Components.Wings
{
    /// <summary>
    /// LLT解析とか翼全体を見たい時に使う
    /// </summary>
    public class Wing : Component
    {
        public int CN { get; set; }
        public int RN { get; set; }
        public double Lift { get; set; }

        public ReactiveProperty<double> CruiseVel { get; set; }

        /// <summary>
        /// 両翼スパン
        /// </summary>
        public double span { get { return span; } set { span = value; le = value / 2; } }
        /// <summary>
        /// 片翼スパン
        /// </summary>
        public double le { get { return le; } set { le = value; span = value * 2; } }

        /// <summary>
        /// 揚力分布
        /// </summary>
        public Distribution LiftDistribution { get; set; }
        /// <summary>
        /// 実効揚力分布
        /// </summary>
        public Distribution TheoricalLiftDistribution { get; set; }

        //楕円分布
        public Distribution EllipseLiftDistribution
        {
            get
            {
                var result = new Distribution();

                return result;
            }
        }

        //public Wing() :base() { }

        public ReactiveCollection<WingSection> PartWings { get; set; } = new ReactiveCollection<WingSection>();       

        public void SectionAdd()
        {
            WingSection ws = new WingSection((Wing)Project.Plane.Wing.Value);
            if(PartWings.Count == 0)
            {
                ws.GlobalPos.Value = new Pos(0, 0, 0);
            }
            else
            {
                WingSection lastSection = (WingSection)PartWings[PartWings.Count - 1];
                ws.GlobalPos.Value = lastSection.GlobalPos.Value +
                    new Pos(lastSection.Length.Value, 0, 0);
            }
            PartWings.Add(ws);
        }

        private Wing() : base()
        {
            BindCollection(PartWings);
        }
        public Wing(Plane parent) : this()
        {
            if(parent != null)
            {
                parent.Wing.Value = this;
            }
        }
    }

}
