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
using HPADesign.Views.Tabs;

namespace HPADesign.Views
{
    
    public sealed partial class MainPage : Page
    {
        
        ConceptViewModel conceptviewmodel { get; }
        AirfoilViewModel airfoilviewmodel { get; } 
        

        public Project project { get; set; }
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;
            project = new Project();

            
            //new AeroDynamicsView(this);
            conceptviewmodel = new ConceptViewModel(project);
            
            airfoilviewmodel = new AirfoilViewModel(project);
            InitializeComponent();

            concept.Project = project;
            concept.Activate();

            aerodynamics.Project = project;
            aerodynamics.Activate();

            airfoil.Project = project;
            airfoil.Activate();

            this.DataContext = this;
        }

    }
}
