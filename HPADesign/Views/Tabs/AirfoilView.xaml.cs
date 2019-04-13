﻿using HPADesign.Models;
using HPADesign.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace HPADesign.Views.Tabs
{
    public sealed partial class AirfoilView : UserControl
    {
        AirfoilViewModel airfoilviewmodel { get; set; }
        public Project Project { get; set; }
        public AirfoilView()
        {
            this.InitializeComponent();
        }

        public void Activate()
        {
            airfoilviewmodel = new AirfoilViewModel(Project);
        }
    }
}