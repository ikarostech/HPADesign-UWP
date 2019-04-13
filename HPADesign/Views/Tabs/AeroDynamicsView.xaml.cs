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

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace HPADesign.Views.Tabs
{
    public sealed partial class AeroDynamicsView : UserControl
    {
        public AeroDynamicsViewModel aerodynamicsviewmodel { get; set; }
        public Project Project { get; set; }
        public AeroDynamicsView()
        {
            this.InitializeComponent();
        }
        public void Activate()
        {
            aerodynamicsviewmodel = new AeroDynamicsViewModel(Project);
        }
    }
}
