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
        public void PlankをChildに連動させる()
        {
            WingSection wingSection = new WingSection(null);
            Plank plank = new Plank(wingSection);
            
            Assert.AreEqual(wingSection.Plank.Value, plank);
        }

        [TestMethod]
        public void TrainingEdgeをChildに連動させる()
        {
            WingSection wingSection = new WingSection(null);
            TrainingEdge trainingEdge = new TrainingEdge(wingSection);
            Assert.AreEqual(wingSection.TrainingEdge.Value, trainingEdge);
        }
    }
}
