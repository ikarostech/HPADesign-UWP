using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Components.Wings
{
    public class Plank : Component
    {
        public ReactiveProperty<double> PlankThin { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// プランク上側取付位置(%)
        /// </summary>
        public ReactiveProperty<double> PlankUpperPos { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// プランク下側取付位置(%)
        /// </summary>
        public ReactiveProperty<double> PlankDownerPos { get; set; } = new ReactiveProperty<double>();

        private Plank()
        {

        }

        public Plank(WingSection parent) : this()
        {
            parent.Plank.Value = this;
        }
    }

}
