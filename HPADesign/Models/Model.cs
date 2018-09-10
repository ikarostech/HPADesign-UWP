using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
    public class Rib
    {
        //リブの名前
        public double Name { get; set; }

        //翼弦長(mm)
        public double Chord { get; set; }
        //ねじり角(deg)
        public double Twist { get; set; }
        //たわみ角(deg)
        public double Dihedral { get; set; }

        //プランク厚(mm)
        public double PlankThin { get; set; }

        //プランク取付位置
        public Tuple<double, double> PlankPos { get; set; }
        //ストリンガー位置(mm)
        public double StringerPos { get; set; }

        //ストリンガー横厚(mm)
        public double StringerWidth { get; set; }

        //ストリンガー縦厚(mm)
        public double StringerHeight { get; set; }

        //後縁キャップ厚(mm)
        public double TrailingEdgeLidThin { get; set; }

        //後縁切り取り位置(mm)
        public double TrainlingEdgeCutPos { get; set; }

        //桁穴位置
        public Tuple<double, double> Sparholepos { get; set; }


        public IAirfoil Foil { get; set; }

        public void RibDXF()
        {
            //ファイルを開く
            var sw = new StreamWriter(new FileStream(Name + ".dxf", FileMode.Open), Encoding.ASCII);

            var OuterFoil = new List<double[]>();


            //まず拡大
            for (int i = 0; i < Foil.Coordinate.Coordinate321.Count; i++)
            {
                var o_point = new double[2] { Foil.Coordinate321[i].x * Chord, Foil.Coordinate321[i].y * Chord };

                OuterFoil.Add(o_point);
            }
            //プランク部のところ


            //ヘッダーを書き込む
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("ENTITIES");
            sw.Dispose();
        }
    }
}
