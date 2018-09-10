using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using System.ComponentModel;

namespace HPADesign.Models
{
    /// <summary>
    /// LLT解析とか翼全体を見たい時に使う
    /// </summary>
    public class Wing : INotifyPropertyChanged
    {
        /// <summary>
        /// 部分翼
        /// </summary>
        public List<PartWing> partWings { get; set; }

        public int CN { get; set; }
        public int RN { get; set; }
        public double Lift { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private double cruisevel;
        public double CruiseVel
        { 
            get { return this.cruisevel; }
            set
            {
                cruisevel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CruiseVel"));
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
    }

}
