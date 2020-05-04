using System.Collections.ObjectModel;
using HPADesign.Models.Component;
using Reactive.Bindings;

namespace HPADesign.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class Project
    {
        public static Plane Plane { get; set; }

        public static ReactiveCollection<Airfoil> Airfoil { get; set; } = new ReactiveCollection<Airfoil>();
    }
}
