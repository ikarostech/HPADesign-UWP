using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models.Components.Wings;
using HPADesign.Models;

namespace HPADesignTest.Models.Components.Wings
{
    [TestClass]
    public class WingTest
    {
        [TestMethod]
        public void Componentのコンストラクタを実行する()
        {
            Wing wing = new Wing(null);
            wing.GlobalPos.Value = new Pos(1, 1, 1);
            Assert.AreEqual(wing.GlobalPos.Value, wing.LocalPos.Value);
        }
        [TestMethod]
        public void WingSectionを子に持つ()
        {
            Wing wing = new Wing(null);
            WingSection wingSection = new WingSection(wing);
            Assert.IsNotNull(wing.PartWings.Where(x => x.Equals(wingSection)).FirstOrDefault());
        }
    }
}
