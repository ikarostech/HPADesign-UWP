using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Models.Components.Wings;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Uwp;


namespace HPADesign.ViewModels.ComponentViewModel
{
    public class PartWingViewModel
    {
        public ReadOnlyReactiveCollection<RibViewModel> Ribs { get; set; }
        public ReadOnlyReactiveCollection<ObservablePoint> CLPartGraphData { get; set; }
        public ReactiveProperty<LineSeries> CLPartGraph { get; set; } = new ReactiveProperty<LineSeries>();
       
        public PartWingViewModel(WingSection partWing)
        {
            Ribs = partWing.Ribs.ToReadOnlyReactiveCollection(x => new RibViewModel(x));
            CLPartGraph.Value = new LineSeries();
            CLPartGraph.Value.Values = new ChartValues<ObservablePoint>();
            CLPartGraphData = partWing.Ribs
                .ToReadOnlyReactiveCollection(
                x => new ObservablePoint(x.GlobalPos.Value.x,
                x.Chord.Value * x.Airfoil.AirfoilPerformance.CL.Value));
                //1000));
            CLPartGraph.Subscribe(x =>
            {
                CLPartGraph.Value.Values.Clear();
                
                for (int i = 0; i < CLPartGraphData.Count; i++)
                {
                    CLPartGraph.Value.Values.Add(CLPartGraphData[i]);
                }
            });
        }

        public void ExportDXF() { }
    }
}
