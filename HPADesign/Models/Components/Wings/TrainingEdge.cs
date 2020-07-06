using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Components.Wings
{
    /// <summary>
    /// 後縁材
    /// </summary>
    public class TrainingEdge : Component
    {

        /// <summary>
        /// 後縁切り取り位置(mm)
        /// </summary>
        public ReactiveProperty<double> TrainlingEdgeLength { get; set; } = new ReactiveProperty<double>();

        private TrainingEdge()
        {

        }
        public TrainingEdge(WingSection parent) : this()
        {
            parent.TrainingEdge.Value = this;
        }
    }
}
