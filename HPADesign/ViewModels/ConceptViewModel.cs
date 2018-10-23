using System;
using HPADesign.Models;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;


namespace HPADesign.ViewModels
{
    public class ConceptViewModel : Observable
    {
        public Wing WingModel { get; set; }
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        public ReadOnlyReactiveCollection<PartWing> Wingsections { get; set; }
        


        public ReactiveCommand ReadFoil { get; set; }
        public ReactiveCommand AddWingSection { get; set; }
        public ReactiveCommand DelWingSection { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel(Wing wing)
        {
            this.WingModel = wing;
            WingModel.CruiseVel = 11;
            TheoricalSpeed = wing.ToReactivePropertyAsSynchronized(x => x.CruiseVel);

            ReadFoil = new ReactiveCommand();
            ReadFoil.Subscribe(_ => { wing.CruiseVel = 12; });

            Wingsections = WingModel.PartWing.ToReadOnlyReactiveCollection(x => x);
            

            AddWingSection = new ReactiveCommand();
            AddWingSection.Subscribe(_ =>
            {
                var section = new PartWing();
                section.Id = 0;
                section.Length = 3000;

                //wing.partWings.Add(section);
                WingModel.PartWing.Add(section);

            });
        }


    }
}
