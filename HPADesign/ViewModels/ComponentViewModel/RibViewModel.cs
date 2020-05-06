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
            
            Airfoil = ReactiveProperty.FromObject(rib, x => x.Airfoil);
            Airfoils = Project.Airfoil.ToReadOnlyReactiveCollection();
        }
        public ReactiveProperty<double> GlobalPos { get;set; }
        public ReactiveProperty<int> Chord { get; set; }
        public ReactiveProperty<Airfoil> Airfoil { get; set; }
        
        //public ReactiveProperty<string> AirfoilName { get; set; }

        public ReactiveProperty<double> Lrho { get; set; }
        public ReadOnlyReactiveCollection<Airfoil> Airfoils { get; set; }
    }
}
