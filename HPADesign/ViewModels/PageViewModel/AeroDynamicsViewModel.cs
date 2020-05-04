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
        public ReadOnlyReactiveCollection<PartWingViewModel> PartWings { get; set; }
        public CollectionViewSource Ribs { get; set; }
        public ReadOnlyReactiveCollection<RibViewModel> RibGraph { get; set; }
        /// <summary>
        /// DataGridのバグでGroup化されたComponentが更新されないので更新用のCommandを別に用意
        /// </summary>
        
        //public ReactiveProperty<CollectionViewSource> Ribs { get; set; }

        public AeroDynamicsViewModel()
        {
            //PartWings = Project.Plane.Wing.Value.PartWings.ToReadOnlyReactiveCollection( x => new PartWingViewModel(x) );

            Ribs = new CollectionViewSource();
            Ribs.IsSourceGrouped = true;
            Ribs.Source = Project.Plane.Wing.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel(x) );
            Ribs.ItemsPath = new Windows.UI.Xaml.PropertyPath("Ribs");
            
        }

        public void Test()
        {
            var test = Ribs.View;
            Console.WriteLine(test);
        }

    }
}
