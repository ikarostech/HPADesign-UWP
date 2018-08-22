using System;
using HPADesign.Models;
using HPADesign.Helpers;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace HPADesign.ViewModels
{
    public class ConceptViewModel : Observable
    {
        public ReactiveProperty<double> TheoricalSpeed { get; set; } = new ReactiveProperty<double>(10);

        public ReactiveCollection<PartWing> Wingsections { get; set; }
        


        public ReactiveCommand ReadFoil { get; set; }
        public ReactiveCommand AddWingSection { get; set; }
        public ReactiveCommand DelWingSection { get; set; }

        //Bindingの初期設定など
        public ConceptViewModel()
        {
            Wing wing = new Wing();
            wing.CruiseVel = 11;
            TheoricalSpeed = wing.ToReactivePropertyAsSynchronized(x => x.CruiseVel);

            ReadFoil = new ReactiveCommand();
            ReadFoil.Subscribe(_ => { wing.CruiseVel = 12; });

            Wingsections = new ReactiveCollection<PartWing>();
            

            AddWingSection = new ReactiveCommand();
            AddWingSection.Subscribe(_ =>
            {
                var section = new PartWing();
                section.Id = 0;
                //section.Length = 3000;
                
                Wingsections.Add(section);
            });
        }


    }
}
