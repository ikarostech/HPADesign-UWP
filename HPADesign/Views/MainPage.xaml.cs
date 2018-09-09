using System;

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
        Wing wing { get; set; }
        ConceptViewModel conceptviewmodel { get; } = new ConceptViewModel();
        AirfoilViewModel airfoilviewmodel { get; } = new AirfoilViewModel();
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;

            
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
