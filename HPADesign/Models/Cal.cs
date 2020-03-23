using System;
using System.Collections.Generic;

namespace HPADesign.Models
{  
    static class Cal
    {
        public static double rho { get { return 1.19; } }
        //線形補完関数
        public static double Lerp(double data1, double data2, double rate)
        {
            return data1 + (data2 - data1) * rate;
        }

        public static double Lerp(Pos p1, Pos p2, double target)
        {
            double rate = (target - p1.x) / (p2.x - p1.x);
            return Lerp(p1.y, p2.y, rate);
        }
        //座標点データから
        public static double Value(List<Pos> point, double target)
        {
            for (int i = 0; i < point.Count - 1; i++)
            {
                if (point[i].x <= target && target < point[i + 1].x)
                {
                    return Lerp(point[i], point[i + 1], target);
                }
            }
            //できなかったらNaNを投げる
            return double.NaN;
        }

        public static double Value(List<Pos> point, double target, int start)
        {
            for (int i = start; i < point.Count - 1; i++)
            {
                if (point[i].x <= target && target < point[i + 1].x)
                {
                    return Lerp(point[i], point[i + 1], target);
                }
            }
            //できなかったらNaNを投げる
            return double.NaN;
        }

        //MATLAB linspace
        public static double[] linspace(double a, double b, int N)
        {
            double[] result = new double[N];
            double delta = (b - a) / (N - 1);
            for (int i = 0; i < N; i++)
            {
                result[i] = a + i * delta;
            }
            return result;
        }

        public static double Cos(double arg)
        {
            double result = Math.Cos(arg / 180 * Math.PI);
            return result;
        }
        public static double Sin(double arg)
        {
            double result = Math.Sin(arg / 180 * Math.PI);
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns> dx / dy </returns>
        public static List<double> Differential(List<Pos> data)
        {
            var result = new List<double>(data.Count);
            Func<int, int, double> diff = (i, j) => { return (data[j].y - data[i].y) / (data[j].x - data[i].x); };
            result.Add(diff(0,1));

            for(int i=1; i<data.Count-1; i++)
            {
                result.Add(diff(i-1,i+1));
            }
            result.Add(diff(data.Count - 2, data.Count - 1));
            return result;
        }

        public static List<double> Differential(List<double> x , List<double> y)
        {
            if(x.Count != y.Count)
            {
                throw new Exception();
            }
            int N = x.Count;
            var result = new List<double>(N);
            Func<int, int, double> diff = (i, j) => { return (y[j] - y[i]) / (x[j] - x[i]); };
            result.Add(diff(0, 1));

            for (int i = 1; i < N - 1; i++)
            {
                result.Add(diff(i - 1, i + 1));
            }
            result.Add(diff(N - 2, N - 1));
            return result;
        }
        
    }
}
