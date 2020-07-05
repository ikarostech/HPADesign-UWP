using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;

namespace HPADesignTest
{
    [TestClass]
    public class PosTest
    {
        
        [TestMethod]
        public void VectorとVector同士の内積()
        {
            Vector a = new Vector(new double[] { 1, 2, 3 });
            Vector b = new Vector(new double[] { 4, 5, 6 });

            double expect = 32;
            double actual = Vector.InnerProduct(a, b);

            Assert.AreEqual(expect, actual);
        }
        [TestMethod]
        public void MatrixEqual()
        {
            Matrix a = new Matrix(new double[1, 1] { { 1 } });
            Matrix b = new Matrix(new double[1, 1] { { 1 } });
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void VectorEqual()
        {
            Vector a = new Vector(new double[] { 1, 2 });
            Vector b = new Vector(new double[] { 1, 2 });
            Vector c = new Vector(new double[] { 2, 2 });
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod]
        public void xyz座標の取得()
        {
            Pos a = new Pos(1, 2, 3);
            Assert.AreEqual(1, a.x);
            Assert.AreEqual(2, a.y);
            Assert.AreEqual(3, a.z);
        }
        [TestMethod]
        public void LU分解()
        {
            RegularMatrix A = new RegularMatrix(new double[2, 2]
            {
                { 1, 2 },
                { 3, 4 }
            });
            RegularMatrix expect = new RegularMatrix(new double[2, 2]
            {
                { 1, 2 },
                { 3,-2 }
            });
            RegularMatrix actual = A.LUSeparate;

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void VectorMagnitudeのテスト()
        {
            Vector vector = new Vector(new double[] { 12, 4,3 });
            double expect = 13;
            double actual = vector.Magnitude;
            Assert.AreEqual(expect, actual);
        }
    }
}
