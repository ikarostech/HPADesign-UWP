using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Models.Component;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace HPADesign.ViewModels.ComponentViewModel
{
    public class RibViewModel
    {
        public RibViewModel(Rib rib)
        {
            rib.Airfoil = new Airfoil();
            Chord = rib.Chord.ToReactiveProperty();
            Lrho = rib.Chord.CombineLatest(rib.Airfoil.AirfoilPerformance.CL, (x, y) => x * y).ToReactiveProperty();
            rib.Airfoil.AirfoilPerformance.CL.Value = 1.1;

            CL = rib.Airfoil.AirfoilPerformance.CL.ToReactiveProperty();
            CD = rib.Airfoil.AirfoilPerformance.CD.ToReactiveProperty();
            CM = rib.Airfoil.AirfoilPerformance.CM.ToReactiveProperty();

            AirfoilName = rib.Airfoil.Name.ToReactiveProperty();
            //CD = rib.Airfoil.Value.AirfoilPerformance.Value.CD.ToReactiveProperty();
        }

        public ReactiveProperty<int> Chord { get; set; }
        public ReactiveProperty<string> AirfoilName { get; set; }
        public ReactiveProperty<double> CL { get; set; }
        public ReactiveProperty<double> CD { get; set; }
        public ReactiveProperty<double> CM { get; set; }
        public ReactiveProperty<double> Lrho { get; set; }

    }
}
