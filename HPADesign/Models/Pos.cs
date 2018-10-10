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
        public double[] Entity { get { return new double[3] { x, y, z }; } }
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

        public static double InnerProduct(Pos A,Pos B)
        {
            return A.x * B.x + A.y * B.y + A.z * B.z;
        }
        public static Pos CrossProduct(Pos A,Pos B)
        {
            return new Pos(A.y * B.z - B.y * A.z, A.z * B.x - B.z * A.x, A.x * B.y - B.x * A.y);
        }

        /// <summary>
        /// 原点からの位置ベクトルの大きさを返します
        /// </summary>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(InnerProduct(this,this));
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
        /// 単位ベクトルを返します
        /// </summary>
        /// <returns></returns>
        public Pos UnitVector
        {
            get
            {
                return new Pos(
                    x / Magnitude,
                    y / Magnitude,
                    z / Magnitude);
            }
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
    public class Matrix
    {
        public double[,] Entry { get; set; }
        public int M { get { return Entry.GetLength(0); } }
        public int N { get { return Entry.GetLength(1); } }

        public double det
        {
            get
            {
                Matrix tmp = new Matrix(Entry);
                if(M!=N)
                {
                    //正則行列以外では行列式を定義できない
                    return double.NaN;
                }

                //計算しやすいように行列を入れ替えていくぜ
                for(int i=0; i<N; i++)
                {
                    if(tmp.Entry[i,i]!=0)
                    {
                        continue;
                    }
                    int j;
                    for(j=0; j<N; j++)
                    {
                        if (tmp.Entry[j, i] != 0)
                        {
                            break;
                        }
                    }
                    if(j==N)
                    {
                        //ある列がすべて0→detA=0
                        return 0;
                    }
                    for(int k=0; k<N; k++)
                    {
                        tmp.Entry[i, k] += tmp.Entry[j, k];
                    }
                }
                double result = 1;
                
                for(int i=0; i<N; i++)
                {
                    for(int j=0; j<N; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        double buf = tmp.Entry[j, i] / tmp.Entry[i, i];
                        
                        for(int k=0; k<N; k++)
                        {
                            tmp.Entry[j,k] -= buf * tmp.Entry[i,k];
                        }
                    }
                }
                for(int i=0; i<N; i++)
                {
                    result *= tmp.Entry[i, i];
                }
                return result;
            }
        }

        public Matrix(int m, int n)
        {
            Entry = new double[m, n];
        }
        public Matrix(double[,] vs)
        {
            Entry = vs;
        }

        public static Matrix Zero(int M,int N)
        {
            var result = new Matrix(M, N);
            for(int i=0; i<M; i++)
            {
                for(int j=0; j<N; j++)
                {
                    result.Entry[i, j] = 0;
                }
            }
            return result;
        }
        public static Matrix E(int N)
        {
            var result = Zero(N, N);
            for(int i=0; i<N; i++)
            {
                result.Entry[i, i] = 1;
            }
            return result;
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
            {
                throw new InvalidOperationException();
            }
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    result.Entry[i, j] += B.Entry[i, j];
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
            {
                throw new InvalidOperationException();
            }
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    result.Entry[i, j] -= B.Entry[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix A, double scaler)
        {
            var result = new Matrix(A.Entry);
            for(int i=0; i<A.M; i++)
            {
                for(int j=0; i<A.N; i++)
                {
                    result.Entry[i,j] *= scaler;
                }
            }
            return result;
        }
        public static Matrix operator /(Matrix A, double scaler)
        {
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; i < A.N; i++)
                {
                    result.Entry[i, j] /= scaler;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if(A.N!=B.M)
            {
                throw new InvalidOperationException();
            }
            var result = Zero(A.M,B.N);

            //ループを回します…
            for (int i = 0; i < result.M; i++)
            {
                for (int j = 0; j < result.N; j++)
                {
                    for(int k=0; k< A.N; k++)
                    {
                        result.Entry[i, j] += A.Entry[i, k] * B.Entry[k, j];
                    }
                }
            }
            return result;
        }

        

        public Matrix LUSeparate
        {
            get
            {
                if (M != N)
                {
                    //正則行列でない場合はエラー
                    return Zero(M,N);
                }

                Matrix result = new Matrix(N, N);

                //ここら辺は最適化できそう？？？
                for(int i=0; i<N; i++)
                {
                    //L要素の算出
                    for (int j=0; j<=i; j++)
                    {
                        double lu = Entry[i, j];
                        for(int k=0; k<j; k++)
                        {
                            lu -= Entry[i, k] * Entry[k, j];
                        }
                        result.Entry[i, j] = lu;
                    }
                    //U要素の算出
                    for (int j =i+1; j<N; j++)
                    {
                        double lu = Entry[i, j];
                        for(int k=0; k<i; k++)
                        {
                            lu -= Entry[i, k] * Entry[k, j];
                        }
                        //ここがおっかない
                        ///TODO
                        result.Entry[i, j] = lu / Entry[i, i];
                    }
                }
                return result;
            }
        }
    }

    public class Vector
    {
        double[] Entry { get; set; }
        int N { get { return Entry.Length; } }
        public Vector(int n)
        {
            Entry = new double[n];
        }
        public Vector(double[] vs)
        {
            Entry = vs;
        }
    }
}
