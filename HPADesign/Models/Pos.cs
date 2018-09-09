using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    public class Pos : IComparable
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Pos()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Pos(double x, double y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }
        public Pos(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public bool is2D { get; set; }

        public static Pos operator +(Pos a, Pos b)
        {
            return new Pos(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Pos operator -(Pos a, Pos b)
        {
            return new Pos(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Pos operator *(double a, Pos p)
        {
            return new Pos(a * p.x, a * p.y, a * p.z);
        }
        public static Pos operator /(Pos p, double a)
        {
            return new Pos(p.x / a, p.y / a, p.z / a);
        }

        public double InnerProduct(Pos To)
        {
            return x * To.x + y * To.y + z * To.z;
        }
        public Pos CrossProduct(Pos To)
        {
            return new Pos(y * To.z - To.y * z, z * To.x - To.z * x, x * To.y - To.x * y);
        }

        /// <summary>
        /// 原点からの位置ベクトルの大きさを返します
        /// </summary>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(InnerProduct(this));
            }
        }

        /// <summary>
        /// 原点基準のΘ角(x-y偏角)を返します
        /// </summary>
        public double Theta
        {
            get
            {
                return Math.Atan2(y, x);
            }
        }

        /// <summary>
        /// 2Dベクトルの原点中心の回転を行います
        /// </summary>
        /// <param name="theta"></param>
        /// <returns></returns>
        public Pos Rotation2DVector(double theta)
        {
            return new Pos(
                x * Cal.Cos(theta) - y * Cal.Sin(theta),
                x * Cal.Sin(theta) + y * Cal.Cos(theta)
                );
        }

        /// <summary>
        /// Toベクターへの方向ベクトルを返します
        /// </summary>
        /// <param name="To"></param>
        /// <returns></returns>
        public Pos DirectionVector(Pos To)
        {
            return new Pos(To.x - x, To.y - y, To.z - z);
        }
        /// <summary>
        /// Toベクターへの単位方向ベクトルを返します
        /// </summary>
        /// <param name="To"></param>
        /// <returns></returns>
        public Pos DirectionUnitVector(Pos To)
        {
            Pos Direction = DirectionVector(To);
            return Direction / Direction.Magnitude;
        }
        /// <summary>
        /// 2次元方向ベクトルに対して左方向に向かう法線ベクトルを取得します
        /// </summary>
        /// <param name="To"></param>
        /// <returns></returns>
        public Pos NormalVector(Pos To)
        {
            Pos Direction = DirectionVector(To);
            return new Pos(-Direction.y, Direction.x, 0);
        }
        public Pos NormalUnitVector(Pos To)
        {
            Pos Normal = NormalVector(To);
            return Normal / Normal.Magnitude;
        }

        public int CompareTo(object obj)
        {
            return x.CompareTo(obj);
        }
    }

}
