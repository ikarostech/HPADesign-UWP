using HPADesign.Models;
using HPADesign.Models.Components.Wings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesignTest.Models.Components.Wings
{
    [TestClass]
    public class RibTest
    {
        [TestMethod]
        public void Componentのコンストラクタを実行する()
        {
            Rib rib = new Rib(null);
            rib.GlobalPos.Value = new Pos(1, 1, 1);
            Assert.AreEqual(rib.GlobalPos.Value, rib.LocalPos.Value);
        }
        [TestMethod]
        public void RibCapを子に持つ()
        {
            Rib rib = new Rib(null);
            RibCap ribCap = new RibCap(rib);

            Assert.AreEqual(rib.RibCap.Value, ribCap);
        }
    }
}
