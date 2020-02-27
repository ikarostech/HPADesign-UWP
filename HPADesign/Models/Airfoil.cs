using System;
using System.Collections.Generic;

namespace HPADesign.Models
{
    public enum AirfoilSide { Upper = 1, Downer = -1 }
    public enum CoordinateType { Selig,Lednicer,Null=-1 }
    public interface IAirfoil
    {
        string Name { get; set; }
        //CoordinateType Type { get; }
        ICoordinate Coordinate { get; set; }
        List<Pos> Coordinate321 { get; }
        double yValue(double xPos, int AirfoilSide);
        Pos yValue(Pos x, int AirfoilSide);

        Distribution Camber { get; }

        double MaxThickness { get;  }
        double AtMaxThickness { get; }
        double MaxCamber { get;  }
        double AtMaxCamber { get; }
    }

    public interface ICoordinate
    {
        CoordinateType Type { get; }
        List<Pos> Coordinate { get; set; }
        List<Pos> Coordinate321 { get; }
        //Pos Value(double x, AirfoilSide airfoilSide);
    }

    public class SeligCoordinate : ICoordinate
    {
        public CoordinateType Type { get { return CoordinateType.Selig; } }
        public List<Pos> Coordinate { get; set; }
        public List<Pos> Coordinate321          
        {
            get
            {
                var result = new List<Pos>();
                int pr = 0;
                const int N = 160;
                for (int i = N; i >= 0; i--)
                {
                    double x = (double)i / N;
                    while (Coordinate[pr + 1].x > x || Coordinate[pr].x < x)
                    {
                        pr++;
                    }
                    //Lerpする
                    double y = Cal.Lerp(Coordinate[pr], Coordinate[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                for (int i = 1; i <= N; i++)
                {
                    double x = (double)i / N;
                    while ((Coordinate[pr + 1].x <= x || Coordinate[pr].x > x) && pr + 2 != Coordinate.Count)
                    {
                        pr++;
                    }
                    //Lerpする
                    double y = Cal.Lerp(Coordinate[pr], Coordinate[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                return result;
            }
        }
        public Pos Value(double x,AirfoilSide airfoilSide)
        {
            return null;
        }
        /// <summary>
        /// 1->0->1の座標点を0->1,0->1の座標点形式に変換します
        /// </summary>
        /// <returns></returns>
        public LednicerCoordinate Selig2Lednicer()
        {
            var result = new LednicerCoordinate();
            
            //プロパティに直接ぶち込むと正規化されて座標が狂うのでまとめて代入する
            var poses = new List<Pos>();
            int i = 0;
            for (; i < Coordinate.Count; i++)
            {
                if (Coordinate[i].x < 0.001) break;
                poses.Insert(0, Coordinate[i]);
            }
            for (; i < Coordinate.Count; i++)
            {
                poses.Add(Coordinate[i]);
            }
            result.Coordinate = poses;
            return result;
        }
    }
    public class LednicerCoordinate : ICoordinate
    {
        public CoordinateType Type { get { return CoordinateType.Lednicer; } }
        public List<Pos> Coordinate { get; set; }
        public List<Pos> Coordinate321
        {
            get
            {
                var selig = Lednicer2Selig();
                var result = new List<Pos>();
                int pr = 0;
                const int N = 160;
                for (int i = N; i >= 0; i--)
                {
                    double x = (double)i / N;
                    while (selig.Coordinate[pr + 1].x > x || selig.Coordinate[pr].x < x)
                    {
                        pr++;
                    }
                    //Lerpする
                    double y = Cal.Lerp(selig.Coordinate[pr], selig.Coordinate[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                for (int i = 1; i <= N; i++)
                {
                    double x = (double)i / N;
                    while ((Coordinate[pr + 1].x <= x || selig.Coordinate[pr].x > x) && pr + 2 != Coordinate.Count)
                    {
                        pr++;
                    }
                    //Lerpする
                    double y = Cal.Lerp(Coordinate[pr], Coordinate[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                return result;
            }
        }
        public SeligCoordinate Lednicer2Selig()
        {
            var result = new SeligCoordinate();
            int state = 0;
            double tx = Coordinate[0].x;
            result.Coordinate.Add(Coordinate[0]);
            for (int i = 1; i < Coordinate.Count; i++)
            {
                //state切り替え
                if (state == 0 && tx > Coordinate[i].x)
                {
                    state = 1;
                }

                //点の補充
                if (state == 0)
                {
                    result.Coordinate.Insert(0, Coordinate[i]);
                }
                if (state == 1)
                {
                    result.Coordinate.Add(Coordinate[i]);
                }
                tx = Coordinate[i].x;
            }
            return result;
        }
    }
    public class NullCoordinate : ICoordinate
    {
        public CoordinateType Type
        {
            get { return CoordinateType.Null; }
        }
        public List<Pos> Coordinate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Pos> Coordinate321 { get { return null; } }
    }

    public class Airfoil : IAirfoil
    {
        public string Name { get; set; } = "None";

        public ICoordinate Coordinate { get; set; }
        public List<Pos> Coordinate321 { get { return Coordinate.Coordinate321; } }


        public Distribution Camber
        {
            get
            {
                var result = new List<Pos>();
                for(int i=0; i<160; i++)
                {
                    result.Insert(0, new Pos(Coordinate321[i].x, (Coordinate321[i].y + Coordinate321[320 - i].y)/2));
                }
                return null;
            }
        }

        public double MaxThickness
        {
            get
            {
                double result = 0;
                const int N = 160;
                for (int i = 0; i < N; i++)
                {
                    double temp = Coordinate321[i].y - Coordinate321[Coordinate321.Count - i - 1].y;

                    if (temp > result)
                    {
                        result = temp;
                    }
                }
                return result;
            }
        }

        public double AtMaxThickness
        {
            get
            {
                double result = 0;
                const int N = 160;
                for (int i = 0; i < N; i++)
                {
                    double temp = Coordinate321[i].y - Coordinate321[Coordinate321.Count - i - 1].y;

                    if (temp > result)
                    {
                        result = (double)i / N;
                    }
                }
                return result;
            }
        }

        public double MaxCamber
        {
            get
            {
                double result = 0;
                const int N = 160;
                for (int i = 0; i < N; i++)
                {
                    double temp = Coordinate321[i].y + Coordinate321[Coordinate321.Count - i - 1].y;
                    temp /= 2;
                    if (temp > result)
                    {
                        result = temp;
                    }
                }
                return result;
            }
        }

        //TODO
        public double AtMaxCamber
        {
            get
            {
                return 0;
            }
        }

        public double yValue(double xPos, int AirfoilSide)
        {
            throw new NotImplementedException();
        }

        public Pos yValue(Pos x, int AirfoilSide)
        {
            throw new NotImplementedException();
        }

        public static Airfoil Lerp(Airfoil A,Airfoil B,double rate)
        {
            var result = new Airfoil();

            result.Coordinate = new SeligCoordinate();
            for (int i = 0; i < A.Coordinate321.Count; i++)
            {
                result.Coordinate.Coordinate.Add(new Pos(A.Coordinate321[i].x, Cal.Lerp(A.Coordinate321[i].y, B.Coordinate321[i].y, rate)));
            }

            return result;
        }

        public double Area
        {
            get
            {
                double result = 0;
                for (int i = 0; i < Coordinate321.Count - 1; i++)
                {
                    result += Pos.CrossProduct(Coordinate321[i], Coordinate321[i + 1]).Entry[3] / 2;
                }
                return result;
            }
        }

        /// <summary>
        /// 翼型の幾何中心
        /// </summary>
        public Pos Centroid {
            get
            {
                Pos result = new Pos();

                for(int i=0; i<Coordinate321.Count-1; i++)
                {
                    result += Pos.CrossProduct(Coordinate321[i], Coordinate321[i + 1]).Entry[3] *
                        (Coordinate321[i + 1] + Coordinate321[i])  / 6;
                }
                result /= Area;
                return result;
            }
        }

        /// <summary>
        /// 原点を中心とした慣性行列
        /// </summary>
        public Matrix InertiaMatrix
        {
            get
            {
                Matrix result = new Matrix(3, 3);
                double Ixx = 0;
                double Ixy = 0;
                double Iyy = 0;
                for (int i = 0; i < Coordinate321.Count - 1; i++)
                {
                    Pos diff = Coordinate321[i + 1] - Coordinate321[i];
                    Pos Avg = Coordinate321[i + 1] + Coordinate321[i] / 2;

                    //ストークスの定理からそれぞれ求められる
                    double dArea = Avg.y * diff.x;
                    Ixx += Avg.x * Avg.x * dArea;
                    Ixy += Avg.x * Avg.y * dArea / 2;
                    Iyy += Avg.y * Avg.y * dArea / 3;
                }
                result.Entry = new double[3, 3]
                    {
                        { Ixx, Ixy, 0 },
                        { Ixy, Iyy, 0 },
                        { 0, 0, 0 }
                    };
                return result;
            }
        }

        /// <summary>
        /// 重心を中心とした慣性行列
        /// </summary>
        public Matrix PrincipalInertiaMatrix
        {
            get
            {
                Matrix result = InertiaMatrix;
                result.Entry[0, 0] -= Centroid.x * Centroid.x * Area;
                result.Entry[0, 1] -= Centroid.x * Centroid.y * Area;
                result.Entry[1, 0] = result.Entry[0, 1];
                result.Entry[1, 1] -= Centroid.y * Centroid.y * Area;
                return result;
            }
        }

        /// <summary>
        /// 主慣性軸
        /// </summary>
        public Pos PrincipalAxisAngle
        {
            get {
                throw new NotImplementedException();
                return null;
            }
        }
    }


}
