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
        double Area { get; }
        double ArcLength { get; }

    }
    /// <summary>
    /// 凹箇所がない
    /// </summary>
    public class Coordinate : ICoordinate
    {
        public List<Pos> Points { get; set; }
        public double Area
        {
            get
            {
                return Points.Diff((x, y) => Pos.CrossProduct(x, y)).Sum(x => x[3]) / 2;
            }
        }

        public double ArcLength
        {
            get
            {
                return Points.Diff((x, y) => (Pos)x.DirectionVector(y)).Sum(x => x.Magnitude);
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
    }
}
