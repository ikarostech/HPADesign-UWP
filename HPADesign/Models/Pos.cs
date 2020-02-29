using System;
using System.Collections.Generic;

namespace HPADesign.Models
{
    public class Pos : Vector, IComparable
    {
        public enum Axis { x = 0, y = 1, z = 2 }

        public double x
        {
            get
            {
                return Entry[0];
            }
            set
            {
                Entry[0] = value;
            }
        }
        public double y
        {
            get
            {
                return Entry[1];
            }
            set
            {
                Entry[1] = value;
            }
        }
        public double z
        {
            get
            {
                return Entry[2];
            }
            set
            {
                Entry[2] = value;
            }
        }


        public new int N { get { return 3; } }

        public Pos() : base(new double[3] { 0, 0, 0 }) { }

        public Pos(double[] Entry) : base(new double[3] { Entry[0], Entry[1], Entry[2] }) { }

        public Pos(Vector vector) : base(new double[3] { vector.Entry[0], vector.Entry[1], vector.Entry[2] }) { }

        //public Pos(double x, double y) : base(new double[3] { x, y, 0 }){}
        public Pos(double x,double y):base(new double[3] { x, y, 0 }) { }
        public Pos(double x, double y, double z) : base(new double[3] { x, y, z }) { }

        public static Pos operator +(Pos A, Pos B)
        {
            Pos result = new Pos();
            for (int i = 0; i < A.N; i++)
            {
                result.Entry[i] = A.Entry[i] + B.Entry[i];
            }
            return result;
        }

        public static Pos operator -(Pos A, Pos B)
        {
            Pos result = new Pos();
            for (int i = 0; i < A.N; i++)
            {
                result.Entry[i] = A.Entry[i] - B.Entry[i];
            }
            return result;
        }

        public static Pos operator *(double A, Pos B)
        {
            Pos result = new Pos(B.Entry);
            for (int i = 0; i < B.N; i++)
            {
                result.Entry[i] *= A;
            }
            return result;
        }
        public static Pos operator *(Matrix A, Pos x)
        {
            //xを計算のためにMatrixに変換
            Matrix X = new Matrix(1, x.N);
            Matrix Y = A * X;
            Pos result = new Pos();
            for (int i = 0; i < x.N; i++)
            {
                result.Entry[i] = Y.Entry[1, i];
            }
            return result;
        }
        public static Pos operator /(Pos B, double A)
        {
            Pos result = new Pos(B.Entry);
            for (int i = 0; i < B.N; i++)
            {
                result.Entry[i] /= A;
            }
            return result;
        }

        public bool is2D { get; set; }

        public static Pos CrossProduct(Pos A, Pos B)
        {
            return new Pos(A.y * B.z - B.y * A.z, A.z * B.x - B.z * A.x, A.x * B.y - B.x * A.y);
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
        /// 2次元方向ベクトルに対して左方向に向かう法線ベクトルを取得します
        /// </summary>
        /// <param name="To"></param>
        /// <returns></returns>
        public Pos NormalVector(Pos To)
        {
            Pos Direction = (Pos)DirectionVector(To);
            return new Pos(-Direction.y, Direction.x, 0);
        }
        public Pos NormalUnitVector(Pos To)
        {
            Pos Normal = NormalVector(To);
            return (Pos)(Normal / Normal.Magnitude);
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

        /// <summary>
        /// TODO SVM法により行列の回数を求めます
        /// </summary>
        public int rank
        {
            get; set;
        }
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

        public Matrix Map(Func<double,double> func)
        {
            var result = this;
            for(int i=0; i<M; i++)
            {
                for(int j=0; j<N; i++)
                {
                    Entry[i, j] = func(Entry[i, j]);
                }
            }
            return result;
        }

        public static Matrix Rotation2DMatrix(double arg)
        {
            return new Matrix(new double[,]
            {
                { Cal.Cos(arg), -Cal.Sin(arg) ,0 },
                { Cal.Sin(arg),Cal.Cos(arg) ,0 },
                { 0,0,0 }
            });
        }
        /// <summary>
        /// オイラー角における3D回転
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Matrix Rotation3DMatrix(Pos arg)
        {
            Matrix Rx = new Matrix(new double[,]
            {
                { 1,0,0 },
                { 0, Cal.Cos(arg.x),-Cal.Sin(arg.x) },
                { 0, Cal.Sin(arg.x), Cal.Cos(arg.x) }
            });

            Matrix Ry = new Matrix(new double[,]
            {
                { Cal.Cos(arg.y), 0 ,Cal.Sin(arg.y) },
                { 0,1,0},
                { -Cal.Sin(arg.y), 0, Cal.Cos(arg.y) }
            });

            Matrix Rz = new Matrix(new double[,]
            {
                { Cal.Cos(arg.z), -Cal.Sin(arg.z) ,0 },
                { Cal.Sin(arg.z),Cal.Cos(arg.z) ,0 },
                { 0,0,1 }
            });
            return Rz * Rx * Ry;
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
        private Vector Lforward(Vector x)
        {
            Vector y = new Vector(x.N);
            return y;
        }
    }

    public class Vector
    {
        public double[] Entry { get; set; }
        public int N { get { return Entry.Length; } }
        public Vector(int n)
        {
            Entry = new double[n];
        }
        public Vector(double[] vs)
        {
            Entry = vs;
        }

        public static Vector operator + (Vector A ,Vector B)
        {
            if(A.N!=B.N)
            {
                throw new InvalidOperationException();
            }
            Vector result = new Vector(A.N);
            for(int i=0; i<A.N; i++)
            {
                result.Entry[i] = A.Entry[i] + B.Entry[i];
            }
            return result;
        }

        public static Vector operator -(Vector A, Vector B)
        {
            if (A.N != B.N)
            {
                throw new InvalidOperationException();
            }
            Vector result = new Vector(A.N);
            for (int i = 0; i < A.N; i++)
            {
                result.Entry[i] = A.Entry[i] - B.Entry[i];
            }
            return result;
        }

        public static Vector operator *(double A, Vector B)
        {
            Vector result = new Vector(B.Entry);
            for (int i = 0; i < B.N; i++)
            {
                result.Entry[i] *= A;
            }
            return result;
        }
        public static Vector operator *(Matrix A,Vector x)
        {
            //xを計算のためにMatrixに変換
            Matrix X = new Matrix(1, x.N);
            Matrix Y = A * X;
            Vector result = new Vector(x.N);
            for(int i=0; i<x.N; i++)
            {
                result.Entry[i] = Y.Entry[1, i];
            }
            return result;
        }
        public static Vector operator /(Vector B, double A)
        {
            Vector result = new Vector(B.Entry);
            for (int i = 0; i < B.N; i++)
            {
                result.Entry[i] /= A;
            }
            return result;
        }

        public static double InnerProduct(Vector A,Vector B)
        {
            if (A.N != B.N)
            {
                throw new InvalidOperationException();
            }
            double result = 0;
            for (int i = 0; i < A.N; i++)
            {
                result += A.Entry[i] * B.Entry[i];
            }
            return result;
        }
        //Vectorでは一般的な外積は定義できない

        public double Magnitude
        {
            get
            {
                double result = 0;
                for(int i=0; i<N; i++)
                {
                    result += Math.Pow(Entry[i], 2);
                }
                return result;
            }
        }

        public Vector UnitVector
        {
            get
            {
                return new Vector(Entry) / Magnitude;
            }
        }

        public Vector DirectionVector(Vector To)
        {
            return this - To;
        }

        public Vector DirectionUnitVector(Vector To)
        {
            return DirectionVector(To).UnitVector;
        }
        public static List<double> Pos2List(List<Pos> poslist,int axis)
        {
            var result = new List<double>();
            foreach(Pos p in poslist)
            {
                result.Add(p.Entry[axis]);
            }
            return result;
        }

        public Vector Map(Func<double,double> func)
        {
            var result = this;
            for(int i=0; i<N; i++)
            {
                Entry[i] = func(Entry[i]);
            }
            return result;

        }
    }
}
