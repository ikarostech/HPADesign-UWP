using System;
using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Models.Components.Wings;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace HPADesign.ViewModels
{
    public class ConceptViewModel
    {
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        //public ObservableCollection<PartWing> partWings;
        public ReadOnlyReactiveCollection<WingSection> PartWings { get; set; }
        //public ReadOnlyReactiveCollection<PartWing> PartWings { get; set; }
        public ReactiveProperty<int> SelectedItem { get; set; }

        public ReactiveCommand ReadFoil { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel()
        {
            PartWings = Project.Plane.Wing.Value.PartWings.ToReadOnlyReactiveCollection();            
        }

        public void AddPartWing()
        {
            WingSection pw = new WingSection(Project.Plane.Wing.Value);
            
            pw.Length.Value = 3000;
            pw.RibCount.Value = 10;
            pw.Name.Value = PartWingName.getPartWingName(Project.Plane.Wing.Value.PartWings.Count, Project.WingConfig.PartWingNameType);
            pw.AutoRibsGenerate();
            Project.Plane.Wing.Value.PartWings.Add(pw);     
            
        }
    }
}
