using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HPADesign.Models
{
    public enum AirfoilSide { Upper = 1, Downer = -1 }
    public interface IAirfoil
    {
        string Name { get; set; }
        string Type { get; }
        ICoordinate Coordinate { get; set; }

        double yValue(double xPos, int AirfoilSide);
        Pos yValue(Pos x, int AirfoilSide);

        double MaxThickness { get;  }
        double AtMaxThickness { get; }
        double MaxCamber { get;  }
        double AtMaxCamber { get; }
    }

    public interface ICoordinate
    {
        string Type { get; set; }
        List<Pos> Coordinate { get; set; }
        List<Pos> Coordinate321 { get; set; }
    }
    /// <summary>
    /// 実際にインスタンスを生成されるAirfoilについて実装しなければならないインターフェースです
    /// </summary>

    /// <summary>
    /// 特に形式とかの指定が与えられてないやつ
    /// </summary>


    /// <summary>
    /// 1->0->1の座標形式で構成された翼型
    /// </summary>
    public class Seligfoil : IAirfoil
    {
        public string Name { get; set; }

        public string Type { get { return "Selig"; } }
        private List<Pos> coordinate;
        
        /// <summary>
        /// 成型済み座標データ
        /// </summary>
        public List<Pos> Coordinate
        {
            get
            {
                return coordinate;
            }
            set
            {
                coordinate = value;

                //成型

                //Step1 0に一番近い座標を確定
                Pos t = new Pos(2, 2);
                foreach (Pos p in coordinate)
                {
                    if (p.Magnitude < t.Magnitude)
                    {
                        t = p;
                    }
                }

                for (int i = 0; i < coordinate.Count; i++)
                {
                    Pos p = coordinate[i];
                    p -= t;

                }
                double arg = coordinate[0].Theta;
                double Magnitude = coordinate[0].Magnitude;

                for (int i = 0; i < coordinate.Count; i++)
                {
                    Pos p = coordinate[i];
                    p.Rotation2DVector(-arg);
                    p /= Magnitude;
                }
            }
        }
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
                        result = (double)i/N;
                    }
                }
                return result;
            }
        }

        public double AtMaxCamber { get { return 0; } }

        /// <summary>
        /// 1->0->1の座標点を0->1,0->1の座標点形式に変換します
        /// </summary>
        /// <returns></returns>
        public Lednicerfoil Selig2Lednicer()
        {
            var result = new Lednicerfoil();
            result.Name = this.Name;
            //プロパティに直接ぶち込むと正規化されて座標が狂うのでまとめて代入する
            var poses = new List<Pos>();
            int i=0;
            for(; i<Coordinate.Count; i++)
            {
                if (Coordinate[i].x < 0.001 ) break;
                poses.Insert(0,Coordinate[i]);
            }
            for(;i<Coordinate.Count; i++)
            {
                poses.Add(Coordinate[i]);
            }
            result.Coordinate = poses;
            return result;
        }

        public double yValue(double xPos, AirfoilSide AirfoilSide)
        {
            double l, r;
            switch(AirfoilSide)
            {
                case AirfoilSide.Upper:
                    (l, r) = UpperIndex(xPos);
                    break;
                case AirfoilSide Downer:
                    (l, r) = DownerIndex(xPos);
                    break;
            }
            return double.NaN;
            
        }

        private (int,int) UpperIndex(double xPos)
        {
            int r = (int)Math.Floor(xPos * 160);
            int l = (int)Math.Ceiling(xPos * 160);
            return (l, r);
        }

        private (double,double) DownerIndex(double xPos)
        {
            int l = 160 + (int)Math.Floor(xPos * 160);
            int r = 160 + (int)Math.Ceiling(xPos * 160);
            return (l, r);
        }

        public Pos yValue(Pos x, int AirfoilSide)
        {
            throw new NotImplementedException();
        }
    }
    public class Lednicerfoil : IAirfoil
    {
        public string Type { get { return "Lednicer"; } }
        /// <summary>
        /// RawCoordinate
        /// </summary>
        private List<Pos> coordinate;
        public List<Pos> Coordinate
        {
            get
            {
                return coordinate;
            }
            set
            {
                coordinate = value;

                //成型

                //Step1 0に一番近い座標を確定
                Pos t = new Pos(2, 2);
                foreach (Pos p in coordinate)
                {
                    if (p.Magnitude < t.Magnitude)
                    {
                        t = p;
                    }
                }

                for (int i = 0; i < coordinate.Count; i++)
                {
                    Pos p = coordinate[i];
                    p -= t;

                }
                double arg = coordinate[coordinate.Count-1].Theta;
                double Magnitude = coordinate[coordinate.Count-1].Magnitude;

                for (int i = 0; i < coordinate.Count; i++)
                {
                    Pos p = coordinate[i];
                    p.Rotation2DVector(-arg);
                    p /= Magnitude;
                }
            }
        }
        //Todo
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

        public string Name { get; set; }

        

        public double MaxThickness
        {
            get
            {
                double result = 0;
                for(int i=0; i<=161; i++)
                {
                    double temp = Coordinate321[i].y - Coordinate321[161 + i].y;
                    if(result<temp)
                    {
                        result = temp;
                    }
                }
                return result;
            }
        }


        public double AtMaxThickness => throw new NotImplementedException();

        public double MaxCamber => throw new NotImplementedException();

        public double AtMaxCamber => throw new NotImplementedException();
        public Seligfoil Lednicer2Selig()
        {
            Seligfoil result = new Seligfoil();
            int state = 0;
            double tx = Coordinate[0].x;
            result.Coordinate.Add(Coordinate[0]);
            for(int i=1; i< Coordinate.Count; i++)
            {
                //state切り替え
                if (state == 0 && tx>Coordinate[i].x)
                {
                    state = 1;
                }

                //点の補充
                if(state==0)
                {
                    result.Coordinate.Insert(0, Coordinate[i]);
                }
                if(state==1)
                {
                    result.Coordinate.Add(Coordinate[i]);
                }
                tx = Coordinate[i].x;
            }
            return result;
        }
    }
    public class Nullfoil : IAirfoil
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Type { get { return "Null"; } }

        public List<Pos> Coordinate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Pos> Coordinate321 => throw new NotImplementedException();

        public double MaxThickness => throw new NotImplementedException();

        public double AtMaxThickness => throw new NotImplementedException();

        public double MaxCamber => throw new NotImplementedException();

        public double AtMaxCamber => throw new NotImplementedException();
    }

}
