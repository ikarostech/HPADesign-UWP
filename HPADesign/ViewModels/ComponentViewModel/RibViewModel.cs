using HPADesign.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;
using HPADesign.Models.Component;

namespace HPADesign.ViewModels
{
    public class RibViewModel
    {
        private WingRib Model{ get; set; }
        //public ReadOnlyReactiveCollection<string> Airfoil { get; set; }
        public ReadOnlyReactiveCollection<string> AirfoilList { get; set; }
        public ReactiveProperty<double> Chord { get; set; }
        public ReactiveProperty<int> GlobalPos { get; set; }
        public ReactiveProperty<string> Airfoil { get; set; }
        public RibViewModel(WingRib rib)
        {
            Model = rib;
            Chord = Model.ToReactivePropertyAsSynchronized(x => x.Chord);
            GlobalPos = Model.ToReactivePropertyAsSynchronized(x => x.GlobalPosition);

            //AirfoilList = Model.project.Airfoils.ToReadOnlyReactiveCollection(x => x.Name);
            

        }
    }
}
