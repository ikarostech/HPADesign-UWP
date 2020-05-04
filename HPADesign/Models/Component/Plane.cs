using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Component
{
    public class Plane : Component
    {
        public Wing Wing { get; set; }

        public Plane()
        {
            Children = new ReactiveCollection<Component>();
            Wing = new Wing();
            Children.Add(Wing);
        }
    }
}
