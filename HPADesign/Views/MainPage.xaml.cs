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
        
        ConceptViewModel conceptviewmodel { get; }
        AirfoilViewModel airfoilviewmodel { get; } 
        AeroDynamicsViewModel aerodynamicsviewmodel { get; }
        Project project { get; set; }
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;
            project = new Project();


            conceptviewmodel = new ConceptViewModel(project);
            aerodynamicsviewmodel = new AeroDynamicsViewModel(project);
            airfoilviewmodel = new AirfoilViewModel(project);
            InitializeComponent();

            this.DataContext = this;
        }

    }
}
