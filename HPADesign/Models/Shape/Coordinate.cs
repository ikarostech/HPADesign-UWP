using HPADesign.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    public interface ICoordinate
    {
        List<Pos> Points { get; set; }
        List<double> Arc { get; }
    }
    /// <summary>
    /// 凹箇所がない
    /// </summary>
    public class Coordinate : Shape2D, ICoordinate
    {
        public List<Pos> Points { get; set; }
        public override double Area
        {
            get
            {
                return Points.DiffLoop((u, w) => (w.y + u.y) * (w.x - u.x)).Sum() / 2;
            }
        }
        

        public List<double> Arc
        {
            get
            {
                return Points.DiffLoop((x, y) => x.DirectionVector(y).Magnitude).ToList();
            }
        }
        /// <summary>
        /// 翼型の幾何中心
        /// </summary>
        public Pos Centroid
        {
            get
            {
                Pos result = new Pos();
                result = Points.Diff((x, y) => Pos.CrossProduct(x, y).z * (x + y) / 6).Sum();

                result /= Area;
                return result;
            }
        }
        public override Pos GravityCenter
        {
            get { return Centroid; }
        }
        /// <summary>
        /// 原点を中心とした慣性行列
        /// </summary>
        public RegularMatrix InertiaMatrix
        {
            get
            {
                RegularMatrix result = new RegularMatrix(3);
                double Ixx = 0;
                double Ixy = 0;
                double Iyy = 0;
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Pos diff = Points[i + 1] - Points[i];
                    Pos Avg = Points[i + 1] + Points[i] / 2;

                    //ストークスの定理からそれぞれ求められる
                    double dArea = Avg.y * diff.x;
                    Ixx += Avg.x * Avg.x * dArea;
                    Ixy += Avg.x * Avg.y * dArea / 2;
                    Iyy += Avg.y * Avg.y * dArea / 3;
                }/*
                result = new Matrix(new double[3, 3]
                    {
                        { Ixx, Ixy, 0 },
                        { Ixy, Iyy, 0 },
                        { 0, 0, 0 }
                    });*/
                return result;
            }
        }

        /// <summary>
        /// 重心を中心とした慣性行列
        /// </summary>
        public RegularMatrix PrincipalInertiaMatrix
        {
            get
            {
                RegularMatrix result = InertiaMatrix;
                result[0, 0] -= Centroid.x * Centroid.x * Area;
                result[0, 1] -= Centroid.x * Centroid.y * Area;
                result[1, 0] = result[0, 1];
                result[1, 1] -= Centroid.y * Centroid.y * Area;
                return result;
            }
        }

        /// <summary>
        /// 主慣性軸
        /// </summary>
        public Pos PrincipalAxisAngle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Print()
        {
            var content = new List<KeyValuePair<int, string>>();
            content.Add(new KeyValuePair<int, string>(0, "POLYLINE"));
            content.Add(new KeyValuePair<int, string>(8, "0"));
            content.Add(new KeyValuePair<int, string>(66, "1"));
            content.Add(new KeyValuePair<int, string>(70, "1"));

            
            content.Add(new KeyValuePair<int, string>(0 ,"SEQEND"));
            content.Add(new KeyValuePair<int, string>(8, "0"));
            string result = "";
            
            
            return result;
        }
    }
}
