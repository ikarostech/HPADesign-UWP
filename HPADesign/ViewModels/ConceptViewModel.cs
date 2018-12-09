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
        private Project project;
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
        public ConceptViewModel(Project project)
        {

            this.project = project;
            project.Wing.CruiseVel = 11;
            TheoricalSpeed = project.Wing.ToReactivePropertyAsSynchronized(x => x.CruiseVel);

            ReadFoil = new ReactiveCommand();
            ReadFoil.Subscribe(_ => { project.Wing.CruiseVel = 12; });

            PartWings = project.Wing.PartWings.ToReadOnlyReactiveCollection(x => new PartWingViewModel(x));

            AddWingSection = new ReactiveCommand();
            var test = new PartWing(project.Wing);
            test.Id = 0;
            test.Length = 3000;
            test.StartPos = 0;
            //test.

            //wing.partWings.Add(section);
            project.Wing.PartWings.Add(test);
            project.Wing.PartWingLengthUpdate();
            AddWingSection.Subscribe(_ =>
            {
                var section = new PartWing(project.Wing);
                section.Id = 0;
                section.Length = 3000;

                section.StartPos = project.Wing.PartWings[project.Wing.PartWings.Count - 1].EndPos;
                //wing.partWings.Add(section);
                project.Wing.PartWings.Add(section);

                project.Wing.PartWingLengthUpdate();
            });

            
        }


    }
}
