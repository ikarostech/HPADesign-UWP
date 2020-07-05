using System.Collections.ObjectModel;
using HPADesign.Models.Components;
using HPADesign.Models.Config;
using Reactive.Bindings;

namespace HPADesign.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class Project
    {
        public static WingConfig WingConfig { get; set; } = new WingConfig();

        public static Plane Plane { get; set; }

        public static ReactiveCollection<Airfoil> Airfoil { get; set; } = new ReactiveCollection<Airfoil>();
    }
}
