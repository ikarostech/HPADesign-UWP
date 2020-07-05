using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using HPADesign.ViewModels.ComponentViewModel;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Uwp;
using HPADesign.Models.Components.Wings;

namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel
    {

        public CollectionViewSource Ribs { get; set; } = new CollectionViewSource();
        public ReadOnlyReactiveCollection<PartWingViewModel> PartWingViewModels { get; set; }
        //public ObservableCollection<PartWingViewModel> PartWingViewModels { get; set; }
        public ReadOnlyReactiveCollection<RibViewModel> RibGraph { get; set; }
        public ReadOnlyReactiveCollection<Airfoil> Airfoils { get; set; }
        public GroupedCollection<PartWingViewModel, RibViewModel> ribs { get; set; }
            = new GroupedCollection<PartWingViewModel, RibViewModel>();


        public SeriesCollection CLGraph { get; set; } = new SeriesCollection();
        public AeroDynamicsViewModel()
        {
            
            PartWingViewModels = Project.Plane.Wing.Value.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel((WingSection)x));

            //TODO
            //もうちょっとどうにかならん？？（ざっくりとした指示）
            PartWingViewModels.CollectionChangedAsObservable().Subscribe(pw =>
            {
                if (pw.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach(PartWingViewModel item in pw.NewItems)
                    {
                        CLGraph.Add(item.CLPartGraph.Value);
                        foreach(RibViewModel rib in item.Ribs)
                        {
                            ribs.Add(item, rib);
                        }
                    }
                }
            });

            Ribs.Source = ribs;
            Ribs.IsSourceGrouped = true;            

            Airfoils = Project.Airfoil.ToReadOnlyReactiveCollection();

            
        }
        public void Test() { }
    }
}
