using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    public class AerodynamicsCircumustance
    {
        public decimal Re { get; set; }
        public decimal Mc { get; set; }
    }
    public class AerodynamicsPerformance 
    {
        public ReactiveProperty<double> CL { get; set; } = new ReactiveProperty<double>();
        public ReactiveProperty<double> CD { get; set; } = new ReactiveProperty<double>();
        public ReactiveProperty<double> CM { get; set; } = new ReactiveProperty<double>();
    }
    public class AerodynamicsPerformanceData
    {
        public AerodynamicsCircumustance Circumustance { get; set; }
        public double CL { get; set; }
        public double CD { get; set; }
        public double CM { get; set; }
    }
}
