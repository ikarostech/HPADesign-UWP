﻿using System;
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
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel : Observable
    {
        private Project project;

        //private ReadOnlyReactiveCollection<Rib> Ribs {get;set;}
        public ReadOnlyReactiveCollection<RibViewModel> Ribs { get; set; }
        

        public AeroDynamicsViewModel(Project project)
        {
            this.project = project;
            //
            Ribs = project.Wing.Ribs.ToReadOnlyReactiveCollection(x => new RibViewModel(x));

            
        }
    }
}
