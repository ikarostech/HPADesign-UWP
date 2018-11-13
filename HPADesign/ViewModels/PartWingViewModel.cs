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
        

        public PartWingViewModel(PartWing partWing)
        {
            Model = partWing;
            Length = Model.ToReactivePropertyAsSynchronized(x => x.Length);

            //this.parent = parent;
            //this.id = id;

        }

        public void TextBoxChanged()
        {
            return;
        }
    }
}
