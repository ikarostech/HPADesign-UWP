using HPADesign.Models.Shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Airfoils
{
    public interface IAirfoilCoordinate : ICoordinate
    {
        AirfoilType Type { get; }

        int N { get; set; }
        /// <summary>
        /// Selig形式によるN点からなる翼型データを提供します
        /// </summary>
        List<Pos> NormalPoints { get; }

        //IAirfoilCoordinate Leap(IAirfoilCoordinate a, IAirfoilCoordinate b, double rate);
        Distribution Camber { get; }
        Distribution Upper
        {
            get;
        }
        Distribution Downer { get; }
        Distribution Thickness { get; }
    }

    public abstract class AirfoilCoordinate : Coordinate, IAirfoilCoordinate
    {
        public abstract AirfoilType Type { get; }
        public abstract List<Pos> NormalPoints { get; }
        public int N { get; set; } = 161;
        protected abstract List<Pos> Normalize(List<Pos> points);
        public Distribution Upper
        {
            get
            {
                List<Pos> result = NormalPoints.Take(N / 2 + 1).ToList();
                return new Distribution(result);
            }
        }
        public Distribution Downer
        {
            get
            {
                List<Pos> result = NormalPoints.Skip(N / 2).ToList();
                return new Distribution(result);
            }
        }
        public Distribution Camber
        {
            get
            {
                List<Pos> result = new List<Pos>((N + 1) / 2);
                for (int i = 0; i < (N + 1) / 2; i++)
                {
                    result.Add((NormalPoints[i] + NormalPoints[N - i - 1]) / 2);
                }
                return new Distribution(result);
            }
        }
        public Distribution Thickness
        {
            get
            {
                List<Pos> result = new List<Pos>((N + 1) / 2);
                for (int i = 0; i < (N + 1) / 2; i++)
                {
                    result.Add(new Pos((NormalPoints[i].x + NormalPoints[N - i - 1].x) / 2, (NormalPoints[i].y - NormalPoints[N - i - 1].y)));
                }
                return new Distribution(result);
            }
        }
        public static IAirfoilCoordinate Lerp(IAirfoilCoordinate A, IAirfoilCoordinate B, double rate)
        {
            if (A.N != B.N)
            {
                B.N = A.N;
            }
            var result = new SeligCoordinate();


            for (int i = 0; i < A.N; i++)
            {
                result.Points.Add(new Pos(A.NormalPoints[i].x, Cal.Lerp(A.NormalPoints[i].y, B.NormalPoints[i].y, rate)));
            }

            return (IAirfoilCoordinate)result;
        }
    }
    public class SeligCoordinate : AirfoilCoordinate
    {
        public override AirfoilType Type { get { return AirfoilType.Selig; } }


        public override List<Pos> NormalPoints
        {
            get
            {
                List<Pos> result = Normalize(Points);
                
                
                int pr = 0;
                int N = (this.N) / 2;
                for (int i = N; i >= 0; i--)
                {
                    double x = (double)i / N;
                    while (result[pr + 1].x > x || result[pr].x < x)
                    {
                        pr++;
                    }
                    
                    double y = Cal.Lerp(result[pr], result[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                for (int i = 1; i <= N; i++)
                {
                    double x = (double)i / N;
                    while ((result[pr + 1].x <= x || result[pr].x > x) && pr + 2 != result.Count)
                    {
                        pr++;
                    }

                    double y = Cal.Lerp(result[pr], result[pr + 1], x); ;
                    result.Add(new Pos(x, y));
                }
                
                return result;
            }
        }

        protected override List<Pos> Normalize(List<Pos> points)
        {
            List<Pos> result = new List<Pos>(points);

            // 座標群が閉じていない場合、閉じさせる

            if (result.First() != result.Last())
            {
                Pos edge = (result.First() + result.Last()) / 2;
                result.Add(edge);
                result.Insert(0, edge);
            }

            // 原点を作る(deposition)
            int origin_index = Array.IndexOf(result.Select(x => x.Magnitude).ToArray(), result.Min(x => x.Magnitude));
            Pos origin = result[origin_index];

            result = result.Select(x => x - origin).ToList();

            // 回転を修正(derotation)
            double arg = points.First().Theta;
            result = result.Select(x => x.Rotation2DVector(-arg)).ToList();

            // 縮尺を修正(descale)
            double scale = points.First().Magnitude;
            result = result.Select(x => x / scale).ToList();

            // 精度的に合わないので無理やり合わせる…
            result[0] = new Pos(1, 0);
            result[result.Count - 1] = new Pos(1, 0);

            return result;
            //throw new NotImplementedException();
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
            for (; i < Points.Count; i++)
            {
                if (Points[i].x < 0.001) break;
                poses.Insert(0, Points[i]);
            }
            for (; i < Points.Count; i++)
            {
                poses.Add(Points[i]);
            }
            result.Points = poses;
            return result;
        }
    }
    public class LednicerCoordinate : AirfoilCoordinate
    {
        public override AirfoilType Type { get { return AirfoilType.Lednicer; } }
        public override List<Pos> NormalPoints
        {
            get
            {
                var selig = Lednicer2Selig();
                var result = new List<Pos>();
                int pr = 0;
                int N = (this.N + 1) / 2;
                for (int i = N; i >= 0; i--)
                {
                    double x = (double)i / N;
                    while (selig.Points[pr + 1].x > x || selig.Points[pr].x < x)
                    {
                        pr++;
                    }
                    
                    double y = Cal.Lerp(selig.Points[pr], selig.Points[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                for (int i = 1; i <= N; i++)
                {
                    double x = (double)i / N;
                    while ((Points[pr + 1].x <= x || selig.Points[pr].x > x) && pr + 2 != Points.Count)
                    {
                        pr++;
                    }
                    
                    double y = Cal.Lerp(Points[pr], Points[pr + 1], x);
                    result.Add(new Pos(x, y));
                }
                return result;
            }
        }
        public SeligCoordinate Lednicer2Selig()
        {
            var result = new SeligCoordinate();
            int state = 0;
            double tx = Points[0].x;
            result.Points.Add(Points[0]);
            for (int i = 1; i < Points.Count; i++)
            {
                //state切り替え
                if (state == 0 && tx > Points[i].x)
                {
                    state = 1;
                }

                //点の補充
                if (state == 0)
                {
                    result.Points.Insert(0, Points[i]);
                }
                if (state == 1)
                {
                    result.Points.Add(Points[i]);
                }
                tx = Points[i].x;
            }
            return result;
        }

        protected override List<Pos> Normalize(List<Pos> points)
        {
            throw new NotImplementedException();
        }
    }
    public class NullCoordinate : AirfoilCoordinate
    {
        public override AirfoilType Type
        {
            get { return AirfoilType.Null; }
        }


        public override List<Pos> NormalPoints { get { return null; } }

        protected override List<Pos> Normalize(List<Pos> points)
        {
            throw new NotImplementedException();
        }
    }


}
