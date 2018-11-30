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
    public class ConceptViewModel : Observable
    {
        public Wing WingModel { get; set; }
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        /// <summary>
        /// 編集をかけるときはこっち
        /// </summary>
        
        public ReadOnlyReactiveCollection<PartWing> Wingsections { get; set; }
        public ReadOnlyReactiveCollection<PartWingViewModel> PartWings { get; set; }
        


        public ReactiveCommand ReadFoil { get; set; }
        public ReactiveCommand AddWingSection { get; set; }
        public ReactiveCommand DelWingSection { get; set; }

        public RelayCommand EditPartWing { get; set; }
        public ReactiveCommand EditWingSection { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel(Wing wing)
        {
            
            this.WingModel = wing;
            WingModel.CruiseVel = 11;
            TheoricalSpeed = wing.ToReactivePropertyAsSynchronized(x => x.CruiseVel);

            ReadFoil = new ReactiveCommand();
            ReadFoil.Subscribe(_ => { wing.CruiseVel = 12; });

            PartWings = wing.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel(x));

            AddWingSection = new ReactiveCommand();
            var test = new PartWing(WingModel);
            test.Id = 0;
            test.Length = 3000;

            //wing.partWings.Add(section);
            WingModel.PartWings.Add(test);
            AddWingSection.Subscribe(_ =>
            {
                var section = new PartWing(WingModel);
                section.Id = 0;
                section.Length = 3000;

                //wing.partWings.Add(section);
                WingModel.PartWings.Add(section);


            });

            //EditPartWing = new RelayCommand(TextBoxUpdate);
        }


    }
}
