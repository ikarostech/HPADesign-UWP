using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Calculations
{
    public enum OptType { Max, Min }
    public interface IOptimizer
    {
        OptType Type { get; set; }
        Pos Optimize();
    }
}
