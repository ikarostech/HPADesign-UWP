using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Models.Component;

namespace HPADesign.ViewModels.ComponentViewModel
{
    public class PartWingViewModel
    {
        public ReadOnlyReactiveCollection<RibViewModel> Ribs { get; set; }
        public PartWingViewModel(PartWing partWing)
        {
            Ribs = partWing.Ribs.ToReadOnlyReactiveCollection(x => new RibViewModel(x));
        }
    }
}
