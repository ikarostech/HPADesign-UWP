using System;
using HPADesign.Models;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Reactive.Bindings.Binding;
using Microsoft.Xaml.Interactivity;
using Microsoft.Xaml.Interactions.Core;
using Windows.UI.Xaml.Input;

namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel
    {
        private Wing WingModel { get; }

        //private ReadOnlyReactiveCollection<Rib> Ribs {get;set;}
        private ReadOnlyReactiveCollection<ReadOnlyReactiveCollection<Rib>> Ribs { get; set; }
        public AeroDynamicsViewModel(Wing wing)
        {
            WingModel = wing;
            Ribs = new ReadOnlyReactiveCollection<ReadOnlyReactiveCollection<Rib>>();
            foreach(PartWing pw in WingModel)
            {
                
            }
            //Ribs = WingModel.PartWing.SelectMany(x => x.Ribs).ToReadOnlyReactiveCollection();
        }
    }
}
