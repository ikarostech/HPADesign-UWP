using HPADesign.Models;
using HPADesign.Models.Component;
using HPADesign.Helpers;
using Reactive.Bindings;
using Prism.Mvvm;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;

namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel : BindableBase
    {        
        public ReadOnlyReactiveCollection<PartWing> PartWings { get; set; }
        public CollectionViewSource Ribs { get; set; }
        /// <summary>
        /// DataGridのバグでGroup化されたComponentが更新されないので更新用のCommandを別に用意
        /// </summary>
        
        //public ReactiveProperty<CollectionViewSource> Ribs { get; set; }

        public AeroDynamicsViewModel()
        {
            PartWings = Project.Plane.Wing.Value.PartWings.ToReadOnlyReactiveCollection();

            Ribs = new CollectionViewSource();
            Ribs.IsSourceGrouped = true;
            Ribs.Source = Project.Plane.Wing.Value.PartWings;

            Ribs.ItemsPath = new Windows.UI.Xaml.PropertyPath("Ribs");
        }

    }
}
