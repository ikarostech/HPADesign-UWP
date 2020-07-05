using HPADesign.Helpers;
using HPADesign.Models;
using HPADesign.Models.Components;
using HPADesign.Models.Components.Wings;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace HPADesign.Test
{
    public static class TestClass
    {
        public static void TestMethod()
        {
            BindClass bind = new BindClass();
            BoundClass bound = new BoundClass(bind);
            BoundClass bound2 = new BoundClass(bind);
            //Assert.AreEqual(bind.Bound.Value, bound);            
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
