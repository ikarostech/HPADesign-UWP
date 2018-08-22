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
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace HPADesign.Styles
{
    public sealed partial class Plot : UserControl
    {
        ReactiveCollection<Point> points {get; set;}
        public List<double> x { get; set; }
        public List<double> y { get; set; }
        public ReactiveProperty<PointCollection> Points {
            get
            {
                var result = new ReactiveProperty<PointCollection>();
                result.Value = new PointCollection();
                for(int i=0; i<x.Count; i++)
                {
                    result.Value.Add(new Point(x[i], y[i]));
                }
                return result;
            }
        }
        public Plot()
        {
            this.InitializeComponent();
            x = new List<double>();
            y = new List<double>();

            x.Add(100);
            y.Add(10);
            x.Add(100);
            y.Add(20);
        }

        public void plot(List<double> x,List<double> y)
        {
            if(x.Count == y.Count)
            {
                this.x = x;
                this.y = y;
            }

            
        }
    }
}
