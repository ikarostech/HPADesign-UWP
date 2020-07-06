using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Components.Wings
{
    public class RibCap : Component
    {
        private RibCap()
        {

        }
        public RibCap(Rib parent) : this()
        {
            parent.RibCap.Value = this;
        }

        /// <summary>
        /// リブキャップ厚
        /// </summary>
        public ReactiveProperty<double> RibCapThin { get; set; } = new ReactiveProperty<double>();
    }
}
