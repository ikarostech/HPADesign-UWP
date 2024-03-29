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
using HPADesign.Views.Tabs;
using HPADesign.Models.Components;
using HPADesign.Test;

namespace HPADesign.Views
{
    
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            //MainViewModel ViewModel => DataContext as MainViewModel;
            //var project = Singleton<Project>.Instance;

            Project.Plane = new Plane();

            InitializeComponent();

            this.DataContext = this;
            TestClass.TestMethod();
        }
    }
}
