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
        AirfoilViewModel airfoilviewmodel { get; } = new AirfoilViewModel();
        AeroDynamicsViewModel aerodynamicsviewmodel { get; }
        Project project { get; set; }
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;
            project = new Project();
            //test
            Airfoil t1 = new Airfoil();
            t1.Name = "test1";
            Airfoil t2 = new Airfoil();
            t2.Name = "test2";
            project.Airfoils.Add(t1);
            project.Airfoils.Add(t2);

            conceptviewmodel = new ConceptViewModel(project);
            aerodynamicsviewmodel = new AeroDynamicsViewModel(project);
            InitializeComponent();

            this.DataContext = this;
        }

    }
}
