using System;
using HPADesign.Models;
using HPADesign.Models.Component;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace HPADesign.ViewModels
{
    public class ConceptViewModel : PageViewModel
    {
        public Project project { get; }
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        /// <summary>
        /// 編集をかけるときはこっち
        /// </summary>
        
        
        public ReadOnlyReactiveCollection<PartWing> PartWings { get; set; }
        


        public ReactiveCommand ReadFoil { get; set; }
        public ReactiveCommand AddWingSection { get; set; }
        public ReactiveCommand DelWingSection { get; set; }

        public RelayCommand EditPartWing { get; set; }
        public ReactiveCommand EditWingSection { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel(Project project)
        {
            
        }


    }
}
