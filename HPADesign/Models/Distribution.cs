using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    /// <summary>
    /// 座標点に関する演算を定義します。
    /// </summary>
    /// <summary>
    /// 分布を保管し、自動で線形補完します
    /// </summary>
    public class Distribution
    {
        public SortedList<double, double> Data { get; set; }
        public double Value(double x)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data.Values[i] <= x && x <= Data[i])
                {
                    return Cal.Lerp(new double[2] { Data.Keys[i], Data.Values[i] }, new double[2] { Data.Keys[i + 1], Data.Values[i + 1] }, x);
                }
            }
            //範囲外にある時
            return double.NaN;
        }
    }

}
