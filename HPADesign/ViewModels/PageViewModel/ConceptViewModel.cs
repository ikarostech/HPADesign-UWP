using System;
using HPADesign.Models;
using HPADesign.Models.Component;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace HPADesign.ViewModels
{
    public class ConceptViewModel : BindableBase
    {
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        //public ObservableCollection<PartWing> partWings;
        public ReadOnlyReactiveCollection<PartWing> PartWings { get; set; }
        //public ReadOnlyReactiveCollection<PartWing> PartWings { get; set; }
        public ReactiveProperty<int> SelectedItem { get; set; }

        

        public ReactiveCommand ReadFoil { get; set; }
        public ReactiveCommand AddWingSection { get; set; } = new ReactiveCommand();
        public ReactiveCommand DelWingSection { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel()
        {
            PartWings = Project.Plane.Wing.Value.PartWings.ToReadOnlyReactiveCollection();

            
            AddWingSection.Subscribe(_ =>
            {
                AddPartWing();
            });
        }

        public void AddPartWing()
        {
            PartWing pw = new PartWing();
            pw.Length.Value = 3000;
            pw.RibCount.Value = 10;
            Project.Plane.Wing.Value.PartWings.Add(pw);
        }
    }
}
