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
    public class WingSectionTest
    {
        [TestMethod]
        public void Componentのコンストラクタを実行する()
        {
            WingSection wingSection = new WingSection(null);
            wingSection.GlobalPos.Value = new Pos(1, 1, 1);
            Assert.AreEqual(wingSection.GlobalPos.Value, wingSection.LocalPos.Value);
        }

        [TestMethod]
        public void Ribを子に持つ()
        {
            WingSection wingSection = new WingSection(null);
            Rib rib = new Rib(wingSection);

            Assert.AreEqual(wingSection.Ribs.FirstOrDefault(),rib);
        }

        [TestMethod]
        public void Plankを子に持つ()
        {
            WingSection wingSection = new WingSection(null);
            Plank plank = new Plank(wingSection);
            
            Assert.AreEqual(wingSection.Plank.Value, plank);
        }

        [TestMethod]
        public void TrainingEdgeを子に持つ()
        {
            WingSection wingSection = new WingSection(null);
            TrainingEdge trainingEdge = new TrainingEdge(wingSection);
            Assert.AreEqual(wingSection.TrainingEdge.Value, trainingEdge);
        }
    }
}
