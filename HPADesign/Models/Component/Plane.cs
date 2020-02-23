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
        public ReactiveProperty<Wing> Wing { get; set; }

        public Plane()
        {
            Wing = new ReactiveProperty<Wing>(new Wing());
            Children.Add(Wing.Value);
        }
        //public Plane(Project project) : base(project) { }
    }
}
