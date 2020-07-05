using HPADesign.Helpers;
using HPADesign.IO;
using HPADesign.IO.Components;
using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Models.Shape;
using HPADesign.Models.Components.Wings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

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

        [TestMethod]
        public void Ribの出力_リブ外形を構築()
        {
            WingSection partWing = new WingSection(null);

            Plank plank = new Plank(partWing);
            plank.PlankThin.Value = 2;
            plank.PlankUpperPos.Value = 0.4;
            plank.PlankDownerPos.Value = 0.2;

            Stringer stringer1 = new Stringer(partWing);
            stringer1.AirfoilSide.Value = AirfoilSide.Upper;
            stringer1.StringerPos.Value = 0.4;
            stringer1.StringerHeight.Value = 4;
            stringer1.StringerWidth.Value = 2;

            Stringer stringer2 = new Stringer(partWing);
            stringer2.AirfoilSide.Value = AirfoilSide.Upper;
            stringer2.StringerPos.Value = 0.2;
            stringer2.StringerHeight.Value = 4;
            stringer2.StringerWidth.Value = 2;

            Stringer stringer3 = new Stringer(partWing);
            stringer3.AirfoilSide.Value = AirfoilSide.Downer;
            stringer3.StringerPos.Value = 0.2;
            stringer3.StringerHeight.Value = 4;
            stringer3.StringerWidth.Value = 2;

            Rib rib = new Rib(partWing);
            rib.Chord.Value = 1000;
            rib.RibCap.Value.RibCapThin.Value = 1;
            rib.Airfoil.Coordinate.Points = new List<Pos>()
            {
                { new Pos(1.00000, 0.00060) },
                { new Pos(0.99344, 0.00216) },
                { new Pos(0.98165, 0.00494) },
                { new Pos(0.96782, 0.00820) },
                { new Pos(0.95254, 0.01174) },
                { new Pos(0.93656, 0.01540) },
                { new Pos(0.92034, 0.01904) },
                { new Pos(0.90394, 0.02264) },
                { new Pos(0.88745, 0.02620) },
                { new Pos(0.87096, 0.02969) },
                { new Pos(0.85444, 0.03311) },
                { new Pos(0.83791, 0.03647) },
                { new Pos(0.82138, 0.03975) },
                { new Pos(0.80484, 0.04296) },
                { new Pos(0.78829, 0.04609) },
                { new Pos(0.77175, 0.04914) },
                { new Pos(0.75520, 0.05211) },
                { new Pos(0.73864, 0.05500) },
                { new Pos(0.72207, 0.05782) },
                { new Pos(0.70549, 0.06054) },
                { new Pos(0.68890, 0.06319) },
                { new Pos(0.67234, 0.06575) },
                { new Pos(0.65579, 0.06822) },
                { new Pos(0.63925, 0.07058) },
                { new Pos(0.62277, 0.07284) },
                { new Pos(0.60630, 0.07497) },
                { new Pos(0.58980, 0.07700) },
                { new Pos(0.57336, 0.07890) },
                { new Pos(0.55689, 0.08068) },
                { new Pos(0.54041, 0.08233) },
                { new Pos(0.52391, 0.08386) },
                { new Pos(0.50740, 0.08528) },
                { new Pos(0.49093, 0.08658) },
                { new Pos(0.47448, 0.08773) },
                { new Pos(0.45804, 0.08876) },
                { new Pos(0.44164, 0.08964) },
                { new Pos(0.42531, 0.09037) },
                { new Pos(0.40900, 0.09093) },
                { new Pos(0.39270, 0.09133) },
                { new Pos(0.37637, 0.09156) },
                { new Pos(0.36001, 0.09163) },
                { new Pos(0.34360, 0.09155) },
                { new Pos(0.32714, 0.09133) },
                { new Pos(0.31066, 0.09097) },
                { new Pos(0.29417, 0.09050) },
                { new Pos(0.27783, 0.08991) },
                { new Pos(0.26173, 0.08917) },
                { new Pos(0.24588, 0.08824) },
                { new Pos(0.23032, 0.08707) },
                { new Pos(0.21499, 0.08564) },
                { new Pos(0.19990, 0.08391) },
                { new Pos(0.18499, 0.08184) },
                { new Pos(0.17016, 0.07943) },
                { new Pos(0.15538, 0.07667) },
                { new Pos(0.14076, 0.07361) },
                { new Pos(0.12636, 0.07023) },
                { new Pos(0.11227, 0.06655) },
                { new Pos(0.09860, 0.06257) },
                { new Pos(0.08537, 0.05829) },
                { new Pos(0.07278, 0.05382) },
                { new Pos(0.06126, 0.04929) },
                { new Pos(0.05105, 0.04478) },
                { new Pos(0.04218, 0.04032) },
                { new Pos(0.03460, 0.03598) },
                { new Pos(0.02824, 0.03180) },
                { new Pos(0.02293, 0.02781) },
                { new Pos(0.01848, 0.02403) },
                { new Pos(0.01473, 0.02054) },
                { new Pos(0.01155, 0.01741) },
                { new Pos(0.00885, 0.01465) },
                { new Pos(0.00660, 0.01217) },
                { new Pos(0.00472, 0.00989) },
                { new Pos(0.00318, 0.00774) },
                { new Pos(0.00194, 0.00569) },
                { new Pos(0.00100, 0.00372) },
                { new Pos(0.00035, 0.00181) },
                { new Pos(-0.00000, -0.00003) },
                { new Pos(-0.00004, -0.00185) },
                { new Pos(0.00022, -0.00368) },
                { new Pos(0.00082, -0.00552) },
                { new Pos(0.00173, -0.00735) },
                { new Pos(0.00293, -0.00918) },
                { new Pos(0.00443, -0.01100) },
                { new Pos(0.00623, -0.01280) },
                { new Pos(0.00837, -0.01457) },
                { new Pos(0.01086, -0.01630) },
                { new Pos(0.01379, -0.01790) },
                { new Pos(0.01726, -0.01936) },
                { new Pos(0.02137, -0.02066) },
                { new Pos(0.02632, -0.02186) },
                { new Pos(0.03239, -0.02309) },
                { new Pos(0.03973, -0.02447) },
                { new Pos(0.04842, -0.02584) },
                { new Pos(0.05871, -0.02701) },
                { new Pos(0.07083, -0.02794) },
                { new Pos(0.08476, -0.02870) },
                { new Pos(0.09979, -0.02937) },
                { new Pos(0.11534, -0.02985) },
                { new Pos(0.13129, -0.03015) },
                { new Pos(0.14744, -0.03027) },
                { new Pos(0.16378, -0.03023) },
                { new Pos(0.18028, -0.03005) },
                { new Pos(0.19687, -0.02974) },
                { new Pos(0.21359, -0.02932) },
                { new Pos(0.23047, -0.02883) },
                { new Pos(0.24741, -0.02827) },
                { new Pos(0.26443, -0.02766) },
                { new Pos(0.28157, -0.02701) },
                { new Pos(0.29876, -0.02636) },
                { new Pos(0.31597, -0.02571) },
                { new Pos(0.33319, -0.02507) },
                { new Pos(0.35042, -0.02444) },
                { new Pos(0.36766, -0.02381) },
                { new Pos(0.38492, -0.02318) },
                { new Pos(0.40218, -0.02255) },
                { new Pos(0.41944, -0.02192) },
                { new Pos(0.43669, -0.02129) },
                { new Pos(0.45393, -0.02066) },
                { new Pos(0.47116, -0.02003) },
                { new Pos(0.48838, -0.01939) },
                { new Pos(0.50557, -0.01876) },
                { new Pos(0.52274, -0.01813) },
                { new Pos(0.53990, -0.01749) },
                { new Pos(0.55706, -0.01687) },
                { new Pos(0.57423, -0.01623) },
                { new Pos(0.59142, -0.01560) },
                { new Pos(0.60864, -0.01498) },
                { new Pos(0.62583, -0.01434) },
                { new Pos(0.64299, -0.01371) },
                { new Pos(0.66015, -0.01308) },
                { new Pos(0.67729, -0.01245) },
                { new Pos(0.69444, -0.01182) },
                { new Pos(0.71158, -0.01119) },
                { new Pos(0.72873, -0.01056) },
                { new Pos(0.74587, -0.00993) },
                { new Pos(0.76302, -0.00930) },
                { new Pos(0.78018, -0.00867) },
                { new Pos(0.79735, -0.00804) },
                { new Pos(0.81454, -0.00741) },
                { new Pos(0.83176, -0.00678) },
                { new Pos(0.84894, -0.00615) },
                { new Pos(0.86610, -0.00552) },
                { new Pos(0.88324, -0.00489) },
                { new Pos(0.90035, -0.00426) },
                { new Pos(0.91741, -0.00364) },
                { new Pos(0.93432, -0.00301) },
                { new Pos(0.95093, -0.00240) },
                { new Pos(0.96680, -0.00182) },
                { new Pos(0.98113, -0.00129) },
                { new Pos(0.99325, -0.00085) },
                { new Pos(1.00000, -0.00060) }
            };


            partWing.Plank.Value = plank;
            partWing.Stringers.Add(stringer1);
            partWing.Stringers.Add(stringer2);
            partWing.Stringers.Add(stringer3);
            partWing.TrainingEdge.Value.TrainlingEdgeLength.Value = 25;
            //partWing.Children.Add(rib);
            rib.Parent.Value = partWing;
            string actual = DXF.Content(rib);
            StreamReader sr = new StreamReader("Resource/DXF/Rib.dxf");
            string expect = sr.ReadToEnd();
            sr.Close();
            Assert.AreEqual(actual, expect);
        }
    }
}
