using HPADesign.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;

namespace HPADesign.ViewModels
{
    public class RibViewModel
    {
        private Rib Model{ get; set; }
        //public ReadOnlyReactiveCollection<string> Airfoil { get; set; }
        public ReadOnlyReactiveCollection<string> test { get; set; }
        public ReactiveProperty<double> Chord { get; set; }
        public ReactiveProperty<int> GlobalPos { get; set; }
        public ReactiveProperty<string> Airfoil { get; set; }
        public RibViewModel(Rib rib)
        {
            Model = rib;
            Chord = Model.ToReactivePropertyAsSynchronized(x => x.Chord);
            GlobalPos = Model.ToReactivePropertyAsSynchronized(x => x.GlobalPosition);

            test = Model.project.Airfoils.ToReadOnlyReactiveCollection(x => x.Name);
        }
    }
}
