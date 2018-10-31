﻿using System;

using HPADesign.ViewModels;
using HPADesign.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HPADesign.Models;

namespace HPADesign.Views
{
    public sealed partial class MainPage : Page
    {
        public Wing wing { get; set; }
        ConceptViewModel conceptviewmodel { get; }
        AirfoilViewModel airfoilviewmodel { get; } = new AirfoilViewModel();
        PartWingViewModel partwingeditviewmodel { get; } = new PartWingViewModel();
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;
            wing = new Wing();
            conceptviewmodel = new ConceptViewModel(wing);
            
            InitializeComponent();

            this.DataContext = this;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PartWing p = new PartWing();
            p.Length = 200;
            wing.PartWing.Add(p);
        }
    }
}
