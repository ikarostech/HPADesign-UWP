using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using Reactive.Bindings;
using Reactive.Bindings.Binding;
using Reactive.Bindings.Extensions;

namespace HPADesign.ViewModels
{
    public class RibViewModel
    {
        private Rib Model{ get; set; }
        public ReactiveProperty<double> Chord { get; set; }
        public ReactiveProperty<int> GlobalPos { get; set; }
        public RibViewModel(Rib rib)
        {
            Model = rib;
            Chord = Model.ToReactivePropertyAsSynchronized(x => x.Chord);
            GlobalPos = Model.ToReactivePropertyAsSynchronized(x => x.GlobalPosition);
        }
    }
}
