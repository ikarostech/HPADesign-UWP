using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Helpers
{
    public static class LinqExtention
    {
        public static List<T> Diff<T>(this List<T> source, Func<T, T, T> f)
        {
            var result = new List<T>(source.Count - 1);
            for (int i = 0; i < source.Count - 1; i++)
            {
                result.Add(f(source[i + 1], source[i]));
            }
            return result;
        }
        public static List<double> Diff<T>(this List<T> source, Func<T, T, double> f)
        {
            var result = new List<double>(source.Count - 1);
            for (int i = 0; i < source.Count - 1; i++)
            {
                result.Add(f(source[i + 1], source[i]));
            }
            return result;
        }

        public static Pos Sum(this List<Pos> source)
        {
            var result = new Pos();
            for(int i=0; i<source.Count; i++)
            {
                result += source[i];
            }
            return result;
        }

        public static List<T> DiffLoop<T>(this List<T> source, Func<T, T, T> f)
        {
            var result = new List<T>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                int j = (i + 1) % source.Count;
                result.Add(f(source[j], source[i]));
            }
            return result;
        }

        public static List<double> DiffLoop<T>(this List<T> source, Func<T, T, double> f)
        {
            var result = new List<double>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                int j = (i + 1) % source.Count;
                result.Add(f(source[j], source[i]));
            }
            return result;
        }


    }
}
