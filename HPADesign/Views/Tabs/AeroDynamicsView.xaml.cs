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
using HPADesign.Models;
using HPADesign.ViewModels;
using LiveCharts.Uwp;
using LiveCharts;
using Microsoft.Toolkit.Uwp.UI.Controls;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace HPADesign.Views.Tabs
{
    public sealed partial class AeroDynamicsView : UserControl
    {
        public AeroDynamicsViewModel aerodynamicsviewmodel { get; }
        public AeroDynamicsView()
        {
            aerodynamicsviewmodel = new AeroDynamicsViewModel();
            this.InitializeComponent();
            
            DataContext = aerodynamicsviewmodel;
        }

        private void DataGrid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as DataGrid);
        }
    }
}
