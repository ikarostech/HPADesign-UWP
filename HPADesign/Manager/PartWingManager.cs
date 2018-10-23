using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;

namespace HPADesign.Manager
{
    class PartWingManager
    {
        public static PartWingManager Current { get; } = new PartWingManager();

        private ObservableCollection<PartWing> PartWingSource { get; } = new ObservableCollection<PartWing>();

        public ReadOnlyObservableCollection<PartWing> PartWing { get; }

        public PartWingManager()
        {
            var souce = Enumerable.Select(x=> new PartWing { Length =$""})
        }
    }
}
