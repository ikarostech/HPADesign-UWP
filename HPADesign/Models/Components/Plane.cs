using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models.Components.Wings;

namespace HPADesign.Models.Components
{
    public class Plane : Component
    {
        public Plane() : base()
        {
            Wing.Value = new Wing(this);
        }

        public ReactiveProperty<Wing> Wing { get; set; } = new ReactiveProperty<Wing>();
        public ReactiveProperty<Propeller> Propeller { get; set; } = new ReactiveProperty<Propeller>();
    }
}
