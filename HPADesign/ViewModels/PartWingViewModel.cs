using Reactive.Bindings;
using Reactive.Bindings.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using Reactive.Bindings.Extensions;

namespace HPADesign.ViewModels
{
    public class PartWingViewModel
    {
        int id  { get; set; }
        Wing parent  { get; set; }
        PartWing Model { get; set; }
        public ReactiveProperty<int> Length { get; set; }
        public ReactiveProperty<int> RibCount { get; set; }

        public ReactiveProperty<int> MinChord { get; set; }
        public ReactiveProperty<int> MaxChord { get; set; }
        public PartWingViewModel(PartWing partWing)
        {
            Model = partWing;
            Length = Model.ToReactivePropertyAsSynchronized(x => x.Length);
            RibCount = Model.ToReactivePropertyAsSynchronized(x => x.RibCount);

            MinChord = Model.ToReactivePropertyAsSynchronized(x => x.MinChord);
            MaxChord = Model.ToReactivePropertyAsSynchronized(x => x.MaxChord);
        }
    }
}
