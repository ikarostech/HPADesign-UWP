using HPADesign.Models;
using HPADesign.Models.Component;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using HPADesign.ViewModels.ComponentViewModel;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel
    {        
        
        public CollectionViewSource Ribs { get; set; }
        public ReadOnlyReactiveCollection<PartWingViewModel> PartWingViewModels { get; set; }
        public ReadOnlyReactiveCollection<RibViewModel> RibGraph { get; set; }
        public ReadOnlyReactiveCollection<Airfoil> Airfoils { get; set; }

        public AeroDynamicsViewModel()
        {
            Ribs = new CollectionViewSource();
            PartWingViewModels = Project.Plane.Wing.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel(x));
            
            Ribs.IsSourceGrouped = true;
            Ribs.Source = Project.Plane.Wing.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel(x));
            
            Ribs.ItemsPath = new Windows.UI.Xaml.PropertyPath("Ribs");

            Airfoils = Project.Airfoil.ToReadOnlyReactiveCollection();
        }

        public void Test()
        {
            var test = Ribs;
            var test1 = Ribs.View[0];
            Console.WriteLine(test);
        }

    }
}
