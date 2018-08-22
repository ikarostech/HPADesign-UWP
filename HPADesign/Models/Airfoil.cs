using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HPADesign.Models
{
    public enum AirfoilSide { Upper = 1, Downer = -1 }
    public enum CoordinateType { Selig,Lednicer,Null=-1 }
    public interface IAirfoil
    {
        string Name { get; set; }
        CoordinateType Type { get; }
        ICoordinate Coordinate { get; set; }
        List<Pos> Coordiante321 { get; }
        double yValue(double xPos, int AirfoilSide);
        Pos yValue(Pos x, int AirfoilSide);

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

    public class Airfoil : IAirfoil
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CoordinateType Type => throw new NotImplementedException();

        public ICoordinate Coordinate { get; set; }
        public List<Pos> Coordinate321 { get { return Coordinate.Coordinate321; } }
        public double MaxThickness => throw new NotImplementedException();

        public double AtMaxThickness => throw new NotImplementedException();

        public double MaxCamber => throw new NotImplementedException();

        public double AtMaxCamber => throw new NotImplementedException();

        public double yValue(double xPos, int AirfoilSide)
        {
            throw new NotImplementedException();
        }

        public Pos yValue(Pos x, int AirfoilSide)
        {
            throw new NotImplementedException();
        }
    }

    public class Nullfoil : IAirfoil
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CoordinateType Type { get { return CoordinateType.Null; } }

        public ICoordinate Coordinate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double MaxThickness => throw new NotImplementedException();

        public double AtMaxThickness => throw new NotImplementedException();

        public double MaxCamber => throw new NotImplementedException();

        public double AtMaxCamber => throw new NotImplementedException();

        public double yValue(double xPos, int AirfoilSide)
        {
            throw new NotImplementedException();
        }

        public Pos yValue(Pos x, int AirfoilSide)
        {
            throw new NotImplementedException();
        }
    }

}
