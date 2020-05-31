using HPADesign.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesignTest.Helpers
{
    [TestClass]
    public class PriorityQueueTest
    {
        [TestMethod]
        public void 昇順に要素が出てくる()
        {
            PriorityQueue<int,int> pq = new PriorityQueue<int,int>();
            pq.Add(2,2);
            pq.Add(4,4);
            pq.Add(1,1);
            pq.Add(9,9);

            Queue<int> expect = new Queue<int>(new int[] { 1, 2, 4, 9 });
            while (pq.Count()>0)
            {
                int actual = pq.Take();
                Assert.AreEqual(expect.Dequeue(), actual);
            }
        }

        [TestMethod]
        public void 降順に要素が出てくる()
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(false);
            pq.Add(2, 2);
            pq.Add(4, 4);
            pq.Add(1, 1);
            pq.Add(9, 9);

            Queue<int> expect = new Queue<int>(new int[] { 9, 4, 2, 1 });
            while (pq.Count() > 0)
            {
                int actual = pq.Take();
                Assert.AreEqual(expect.Dequeue(), actual);
            }
        }

        [TestMethod]
        [Timeout(2000)]
        public void Nが10の5乗程度の速度テスト()
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(false);
            for(int i=0; i<200000; i++)
            {
                pq.Add(i, i);
            }
            while(pq.Count()>0)
            {
                pq.Take();
            }
        }
    }
}
