using HPADesign.Models.Airfoils;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Components.Wings
{
    public class Stringer : Component
    {
        /// <summary>
        /// ストリンガー取付側
        /// </summary>
        public ReactiveProperty<AirfoilSide> AirfoilSide { get; set; } = new ReactiveProperty<AirfoilSide>();

        /// <summary>
        /// ストリンガー取付位置(%)
        /// </summary>
        public ReactiveProperty<double> StringerPos { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// ストリンガー横厚(mm)
        /// </summary>
        public ReactiveProperty<double> StringerWidth { get; set; } = new ReactiveProperty<double>();

        /// <summary>
        /// ストリンガー縦厚(mm)
        /// </summary>
        public ReactiveProperty<double> StringerHeight { get; set; } = new ReactiveProperty<double>();

        private Stringer()
        {

        }

        public Stringer(WingSection parent) : this()
        {
            parent.Stringers.Add(this);
        }
        public Stringer(WingSection parent, int index) : this()
        {
            parent.Stringers.Insert(index, this);
        }
    }

}
