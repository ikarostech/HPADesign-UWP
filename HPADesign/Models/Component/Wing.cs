using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Prism.Mvvm;
using System.Collections.Generic;
using HPADesign.Models.Component;
using IComponent = HPADesign.Models.Component.IComponent;

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

        

        private double cruisevel;
        public double CruiseVel
        { 
            get { return this.cruisevel; }
            set
            {
                cruisevel = value;
                RaisePropertyChanged(nameof(CruiseVel));
            }
        }
        

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

        public Wing(Project project) :base(project) { }

        public void addPartWing(PartWing partWing)
        {
            
            Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => partWing.PropertyChanged += h,
                h => partWing.PropertyChanged -= h)
                .Subscribe(e =>
                {
                    RaisePropertyChanged(nameof(PartWings));
                    RaisePropertyChanged(nameof(Children));
                });
                
            PartWings.Add(partWing);
        }

        //TODO
        public void Update()
        {
            //Ribs
            Ribs.Clear();
            //データバインディング上あまりよろしくないができないよりはいいのでつけとくぜ
            foreach(PartWing p in PartWings)
            {
                foreach(WingRib r in p.Ribs)
                {
                    Ribs.Add(r);
                }
            }


            for (int i = 1; i < partwings.Count; i++)
            {
                partwings[i].StartPos = partwings[i - 1].EndPos;
            }
        }
        
    }

}
