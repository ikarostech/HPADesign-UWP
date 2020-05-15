using HPADesign.Helpers;
using HPADesign.IO;
using HPADesign.IO.Component;
using HPADesign.Models;
using HPADesign.Models.Shape;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HPADesignTest.Models.IO
{
    class TestPrintable : IPrintable
    {
        public List<IPrintableElement> Shapes { get; set; } = new List<IPrintableElement>();
    }
    [TestClass]
    public class ShapeOutTest
    {
        [TestMethod]
        public void Coordinateの出力_ボックスを構築()
        {
            Coordinate coordinate = new Coordinate();
            coordinate.Points = new List<Pos>()
            {
                new Pos(0,0,0),
                new Pos(1,0,0),
                new Pos(1,1,0),
                new Pos(0,1,0)
            };
            TestPrintable testPrintable = new TestPrintable();
            testPrintable.Shapes.Add(coordinate);
            
            StreamReader sr = new StreamReader("Resource/DXF/Box.dxf");
            string expect = sr.ReadToEnd();
            sr.Close();

            string actual = DXF.Content(testPrintable);

            Assert.AreEqual(expect, actual, true);
            
        }
        [TestMethod]
        public void Circleの出力_円を構築()
        {
            Circle circle = new Circle();
            circle.Center = new Pos(0, 0, 0);
            circle.Radius = 1;
            TestPrintable testPrintable = new TestPrintable();
            testPrintable.Shapes.Add(circle);

            StreamReader sr = new StreamReader("Resource/DXF/Circle.dxf");
            string expect = sr.ReadToEnd();
            sr.Close();

            string actual = DXF.Content(testPrintable);

            Assert.AreEqual(expect, actual, true);
        }
    }
}
