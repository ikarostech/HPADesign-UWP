using HPADesign.Models.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesignTest.Models.Components
{
    public class BindTest
    {
        [TestClass]
        public class BindPropertyTest
        {

            [TestMethod]
            public void BindにBoundクラスを追加()
            {
                BindClass bind = new BindClass();
                BoundClass bound = new BoundClass(bind);

                Assert.AreEqual(bind.Bound.Value, bound);
            }

            [TestMethod]
            public void BindからBoundクラスを削除()
            {
                BindClass bind = new BindClass();
                BoundClass bound = new BoundClass(bind);
                bind.Bound.Value = null;
                Assert.IsNull(bind.Bound.Value);
                Assert.IsNull(bind.Children.Where(x => x is BoundClass).FirstOrDefault());
            }

            [TestMethod]
            public void BindのBoundクラスを置換()
            {
                BindClass bind = new BindClass();
                BoundClass bound1 = new BoundClass(bind);
                BoundClass bound2 = new BoundClass(bind);
                Assert.AreEqual(bound2, bind.Children.Where(x => x is BoundClass).FirstOrDefault());
                Assert.AreEqual(bound2, bind.Bound.Value);
                Assert.IsNull(bind.Children.Where(x => x == bound1).FirstOrDefault());
            }
        }

        [TestClass]
        public class BindCollectionTest
        {
            [TestMethod]
            public void BindにBoundsクラスを追加()
            {
                BindClass bind = new BindClass();
                BoundsClass bound = new BoundsClass(bind);

                Assert.AreEqual(bound, bind.BoundsCollection[0]);
                Assert.AreEqual(1, bind.BoundsCollection.Count);
            }
        }
    }
    class BindClass : Component
    {
        public ReactiveProperty<BoundClass> Bound { get; set; } = new ReactiveProperty<BoundClass>();
        public ReactiveCollection<BoundsClass> BoundsCollection { get; set; } = new ReactiveCollection<BoundsClass>();
        public BindClass() : base(new ReactiveProperty<Component>())
        {
            BindProperty(Bound);
            BindCollection(BoundsCollection);
        }
    }
    class BoundClass : Component
    {
        private BoundClass()
        {

        }
        public BoundClass(BindClass parent) : this()
        {
            parent.Bound.Value = this;
        }
    }
    class BoundsClass : Component
    {
        public BoundsClass(BindClass parent)
        {
            parent.BoundsCollection.Add(this);
        }
        public BoundsClass(BindClass parent, int index)
        {
            parent.BoundsCollection.Insert(index, this);
        }
        //public BoundsClass(BindClass parent) : base(parent) { }
    }
}
