using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Helpers
{
    /**
     * code is here http://ch3000.blog.fc2.com/blog-entry-30.html
     */
    public class PriorityQueue<Tkey, Tvalue> where Tkey : IComparable
    {
        private List<Tkey> keys = new List<Tkey>();
        private List<Tvalue> values = new List<Tvalue>();
        private int size = 0;
        private static bool Descending;
        private static int Compare(Tkey a, Tkey b) { return (Descending ? a.CompareTo(b) : b.CompareTo(a)); }

        public PriorityQueue(bool b = true) { Clear(); Descending = b; }
        public int Count() { return size; }
        public void Clear() { keys = new List<Tkey>(); values = new List<Tvalue>(); size = 0; }
        public void Add(Tkey e, Tvalue v)
        {
            keys.Add(e);
            values.Add(v);
            int x = size;
            size += 1;
            while (x != 0)
            {
                int par = (x % 2 == 0 ? x - 2 : x - 1) / 2;
                if (Compare(keys[par], e) <= 0) break;
                Tkey a = keys[x]; keys[x] = keys[par]; keys[par] = a;
                Tvalue b = values[x]; values[x] = values[par]; values[par] = b;
                x = par;
            }
        }
        public void Poll()
        {
            Tkey k = keys[size - 1];
            Tvalue v = values[size - 1];
            int x = 0;
            size--;
            if (size == 0) { Clear(); return; }
            keys.RemoveAt(size);
            values.RemoveAt(size);
            while (true)
            {
                int l = x * 2 + 1;
                int r = x * 2 + 2;
                if (size - 1 < l) break;
                int y = 0;
                if (size - 1 == l) { y = l; }
                else if (Compare(keys[l], keys[r]) == 1) { y = r; }
                else { y = l; }
                if (Compare(keys[y], k) > 0) break;
                keys[x] = keys[y];
                values[x] = values[y];
                x = y;
            }
            keys[x] = k;
            values[x] = v;
        }
        public Tvalue Take() { Tvalue ret = values[0]; Poll(); return ret; }
        public Tvalue Peek() { return values[0]; }
        public Tkey PeekKey() { return keys[0]; }
        public void Debug()
        {
            Console.Write("key   : ");
            foreach (var k in keys) Console.Write(k + " ");
            Console.WriteLine();
            Console.Write("value : ");
            foreach (var k in values) Console.Write(k + " ");
            Console.WriteLine();
        }
    }
}
