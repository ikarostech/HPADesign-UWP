using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Models.Components.Wings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesignTest.Models.Components
{
    [TestClass]
    public class PlaneTest
    {
        [TestMethod]
        public void Componentのコンストラクタを実行する()
        {
            Plane plane = new Plane();
            plane.GlobalPos.Value = new Pos(1, 1, 1);
            Assert.AreEqual(plane.GlobalPos.Value, plane.LocalPos.Value);
        }

        [TestMethod]
        public void Wingを子に持つ()
        {
            Plane plane = new Plane();
            Wing wing = new Wing(plane);

            Assert.AreEqual(plane.Wing.Value, wing);
        }

        [TestMethod]
        public void Wingを初期コンポーネントとして持つ()
        {
            Plane plane = new Plane();

            Assert.IsNotNull(plane.Wing.Value);
        }
    }
}
