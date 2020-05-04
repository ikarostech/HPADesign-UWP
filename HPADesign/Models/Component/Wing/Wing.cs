using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections.Generic;
using HPADesign.Models.Component;
using IComponent = HPADesign.Models.Component.IComponent;
using Reactive.Bindings;

namespace HPADesign.Models.Component
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

        public ReactiveCollection<PartWing> PartWings { get; set; } = new ReactiveCollection<PartWing>();

        //TODO
        public void Update()
        {
            
        }
        
    }

}
