using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel
    {
        private Wing WingModel { get; }

        private ReactiveCollection<Rib> Ribs {get;set;}
        public AeroDynamicsViewModel(Wing wing)
        {
            WingModel = wing;
            Ribs = WingModel.PartWing.SelectMany(x => x.Ribs).ToReadOnlyReactiveCollection(x=>x);
        }
    }
}
