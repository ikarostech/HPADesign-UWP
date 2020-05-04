using HPADesign.Calculations;
using System;
using System.Collections.Generic;


namespace HPADesign.Models
{
    public interface IDistribution
    {
        Pos Max { get; }
        Pos Min { get; }
        List<Pos> Data { get; set; }
        Pos Value(double x);
        Pos dydx(double x);
    }

    /// <summary>
    /// 座標点に関する演算を定義します。
    /// </summary>
    /// <summary>
    /// 分布を保管し、自動で線形補完します
    /// </summary>
    public class Distribution : IDistribution
    {
        public Distribution(List<Pos> data)
        {
            Data = data;
        }
        public Distribution()
        {

        }
        /// <summary>
        /// 座標点データ
        /// </summary>
        public List<Pos> Data { get; set; }
        public void DataInsert(Pos newdata)
        {
            Data.Add(newdata);
            Data.Sort();
        }
        /// <summary>
        /// 座標点横軸データの任意の点から縦軸データの線形補間データを返します
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public Pos Value(double x)
        {
            
            var result = new Pos(x, double.NaN);
            for (int i = 0; i < Data.Count - 1; i++)
            {
                if (Data[i].x <= x && x < Data[i + 1].x)
                {
                    result.y = Cal.Lerp(Data[i], Data[i + 1], x);
                }
            }
            if(Data[Data.Count-1].x==x)
            {
                result = Data[Data.Count - 1];
            }   
            //できなかったらNaNを投げる
            return result;
        }

        public Pos dydx(double x)
        {
            var result = new Pos(x, double.NaN);
            for (int i = 0; i < Data.Count - 1; i++)
            {
                if (Data[i].x <= x && x < Data[i + 1].x)
                {
                    //ここで二つに分ける
                    if (Data[i].x == x && i > 0)
                    {
                        //座標上ドンピシャなので前後の座標点を基に微分値をたたき出す
                        result.y = (Data[i + 1].y - Data[i - 1].y) / (Data[i + 1].x - Data[i - 1].x);
                    }
                    else if(Data[i].x!=x)
                    {
                        //座標点と座標点の間にあるのでそこの傾きを返す
                        result.y = (Data[i].y - Data[i - 1].y) / (Data[i].x - Data[i - 1].x);
                    }
                }
            }
            return result;
        }

        private IOptimizer optimizer;
        public Pos Max
        {
            get
            {
                return optimizer.Optimize();
            }
        }
        public Pos Min
        {
            get
            {
                return optimizer.Optimize();
            }
        }
    }
    
}
