using System;
using System.Collections;
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

        //public Pos() : base(new double[3] { 0, 0, 0 }) { }

        //public Pos(double[] Entry) : base(new List<double>(new double[3] { Entry[0], Entry[1], Entry[2] })) {
        public Pos()
        {
            Entry = new List<double>(new double[3]);
        }
        public Pos(Vector vector) : base(new List<double>(new double[] { vector[0], vector[1], vector[2] })) { }

        
        public Pos(double x,double y)
        {
            Entry = new List<double>(new double[] { x, y, 0 });
            
        }

        public Pos(double x, double y, double z) : base(3)
        {
            Entry = new List<double>(new double[] { x, y, z });
        }


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
            Pos result = new Pos(B);
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
                result.Entry[i] = Y[1, i];
            }
            return result;
        }
        public static Pos operator /(Pos B, double A)
        {
            Pos result = new Pos(B);
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
        /// 回転
        /// </summary>
        public static Pos Rotation(double theta)
        {
            return new Pos(Cal.Cos(theta), Cal.Sin(theta));
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
            return new Pos(Direction.y, -Direction.x, 0);
        }
        public Pos NormalUnitVector(Pos To)
        {
            Pos Normal = NormalVector(To);
            return (Normal / Normal.Magnitude);
        }

        public Pos Inverse()
        {
            return new Pos(y, -x, 0);
        }


        public int CompareTo(object obj)
        {
            return x.CompareTo(obj);
        }
    }

    public class Matrix
    {
        protected double[,] Entry { get; set; }
        public virtual int M { get { return Entry.GetLength(0); } }
        public int N { get { return Entry.GetLength(1); } }

        public Matrix(int m, int n)
        {
            Entry = new double[m,n];
        }
        public Matrix(double[,] vs)
        {
            Entry = vs;
        }

        public double this[int i, int j] { get => Entry[i, j]; set => Entry[i, j] = value; }


        public static Matrix Zero(int M,int N)
        {
            var result = new Matrix(M, N);
            for(int i=0; i<M; i++)
            {
                for(int j=0; j<N; j++)
                {
                    result[i, j] = 0;
                }
            }
            return result;
        }
        public static Matrix E(int N)
        {
            var result = Zero(N, N);
            for(int i=0; i<N; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
            {
                throw new NotSameShapeException();
            }
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    result[i, j] += B[i, j];
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
            {
                throw new NotSameShapeException();
            }
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    result[i, j] -= B[i, j];
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
                    result[i,j] *= scaler;
                }
            }
            return result;
        }
        public static Matrix operator / (Matrix A, double scaler)
        {
            var result = new Matrix(A.Entry);
            for (int i = 0; i < A.M; i++)
            {
                for (int j = 0; i < A.N; i++)
                {
                    result[i, j] /= scaler;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if(A.N!=B.M)
            {
                throw new NotSameShapeException();
            }
            var result = Zero(A.M,B.N);

            //ループを回します…
            for (int i = 0; i < result.M; i++)
            {
                for (int j = 0; j < result.N; j++)
                {
                    for(int k=0; k< A.N; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
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
                    result[i, j] = func(result[i, j]);
                }
            }
            return result;
        }

        

        public Vector Row(int index)
        {
            var result = new Vector(M);
            for(int i=0; i<M; i++)
            {
                result[i] = Entry[i,index];
            }
            return result;
        }
        public Vector Columns (int index)
        {
            var result = new Vector(N);
            for(int i=0; i<N; i++)
            {
                result[i] = Entry[index,i];
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Matrix))
            {
                return false;
            }
            Matrix counter = obj as Matrix;
            if(M!=counter.M || N!=counter.N)
            {
                return false;
            }
            for(int i=0; i<M; i++)
            {
                for(int j=0; j<N; j++)
                {
                    if (Math.Abs(Entry[i, j] - counter[i, j]) > 1e-6)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
    public class RegularMatrix : Matrix
    {
        public override int M { get { return N; } }
        public RegularMatrix(double[,] vs) : base(vs)
        {
            
            if(vs.GetLength(0)!=vs.GetLength(1))
            {
                throw new NotRegularException();
            }
        }
        public RegularMatrix(int N) : base(N,N)
        {
            
        }

        /// <summary>
        /// TODO SVM法により行列の階数を求めます
        /// </summary>
        public int rank
        {
            get; set;
        }
        public double det
        {
            get
            {
                RegularMatrix tmp = new RegularMatrix(Entry);
                

                //計算しやすいように行列を入れ替えていくぜ
                for (int i = 0; i < N; i++)
                {
                    if (tmp[i, i] != 0)
                    {
                        continue;
                    }
                    int j;
                    for (j = 0; j < N; j++)
                    {
                        if (tmp[j, i] != 0)
                        {
                            break;
                        }
                    }
                    if (j == N)
                    {
                        //ある列がすべて0→detA=0
                        return 0;
                    }
                    for (int k = 0; k < N; k++)
                    {
                        tmp[i, k] += tmp[j, k];
                    }
                }
                double result = 1;

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        double buf = tmp[j, i] / tmp[i, i];

                        for (int k = 0; k < N; k++)
                        {
                            tmp[j, k] -= buf * tmp[i, k];
                        }
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    result *= tmp[i, i];
                }
                return result;
            }
        }
        public static Matrix Rotation2DMatrix(double arg)
        {
            return new Matrix(new double[,]
            {
                { Cal.Cos(arg), -Cal.Sin(arg), 0 },
                { Cal.Sin(arg), Cal.Cos(arg), 0 },
                { 0, 0, 0 }
            });
        }
        /// <summary>
        /// オイラー角における3D回転
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static RegularMatrix Rotation3DMatrix(Pos arg)
        {
            RegularMatrix Rx = new RegularMatrix(new double[,]
            {
                { 1,0,0 },
                { 0, Cal.Cos(arg.x),-Cal.Sin(arg.x) },
                { 0, Cal.Sin(arg.x), Cal.Cos(arg.x) }
            });

            RegularMatrix Ry = new RegularMatrix(new double[,]
            {
                { Cal.Cos(arg.y), 0 ,Cal.Sin(arg.y) },
                { 0,1,0},
                { -Cal.Sin(arg.y), 0, Cal.Cos(arg.y) }
            });

            RegularMatrix Rz = new RegularMatrix(new double[,]
            {
                { Cal.Cos(arg.z), -Cal.Sin(arg.z) ,0 },
                { Cal.Sin(arg.z),Cal.Cos(arg.z) ,0 },
                { 0,0,1 }
            });
            return (RegularMatrix)(Rz * Rx * Ry);
        }

        public RegularMatrix LUSeparate
        {
            get
            {
                RegularMatrix result = new RegularMatrix(N);

                //ここら辺は最適化できそう？？？
                for (int i = 0; i < N; i++)
                {
                    //L要素の算出
                    for (int j = 0; j <= i; j++)
                    {
                        double lu = Entry[i, j];
                        for (int k = 0; k < j; k++)
                        {
                            lu -= Entry[i, k] * Entry[k, j];
                        }
                        result[i, j] = lu;
                    }
                    //U要素の算出
                    for (int j = i + 1; j < N; j++)
                    {
                        double lu = Entry[i, j];
                        for (int k = 0; k < i; k++)
                        {
                            lu -= Entry[i, k] * Entry[k, j];
                        }
                        //ここがおっかない
                        ///TODO
                        result[i, j] = lu / Entry[i, i];
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

    public class NotRegularException : Exception { }
    public class NotSameShapeException : Exception { }
    public class Vector 
    {
        protected List<double> Entry { get; set; }
        public int N { get { return Entry.Count; } }

        protected Vector()
        {
            Entry = new List<double>();
        }
        public Vector(double[] vs)
        {
            Entry = new List<double>(vs);
        }
        public Vector(List<double> vs)
        {
            Entry = vs;
        }
        public Vector(int n)
        {
            Entry = new List<double>(new double[n]);
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
                result[i] = Y[1, i];
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

        public double this[int index] { get { return Entry[index]; } set => Entry[index] = value; }

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

        public IEnumerator GetEnumerator()
        {
            return ((IList<double>)Entry).GetEnumerator();
        }
    }
}
