using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPADesign.Models;
using HPADesign.Models.Components;
using Reactive.Bindings;

namespace HPADesignTest.Models.Components
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        public void ルートのPosを連動させる()
        {
            Component root = new Component(new ReactiveProperty<Component>());
            root.GlobalPos.Value = new Pos(1, 0, 0);
            
            Assert.AreEqual(1, root.LocalPos.Value.Magnitude);

            root.LocalPos.Value = new Pos(0, 0, 0);
            Assert.AreEqual(0, root.GlobalPos.Value.Magnitude);
        }

        [TestMethod]
        public void 子のGlobalPosを取得()
        {
            Component root = new Component(new ReactiveProperty<Component>());
            root.GlobalPos.Value = new Pos(1, 1, 1);

            
            Component child = new Component(new ReactiveProperty<Component>());
            root.Children.Add(child);
            child.Parent.Value = root;
            child.LocalPos.Value = new Pos(1, 1, 1);

            Assert.AreEqual(new Pos(2, 2, 2), child.GlobalPos.Value);
        }

        [TestMethod]
        public void 後から親の位置をずらす()
        {
            Component root = new Component(new ReactiveProperty<Component>());
            Component child = new Component(new ReactiveProperty<Component>());
            Component grandChild = new Component(new ReactiveProperty<Component>());

            root.Children.Add(child);
            child.Parent.Value = root;

            child.Children.Add(grandChild);
            grandChild.Parent.Value = child;

            root.GlobalPos.Value = new Pos(0, 0, 0);
            child.LocalPos.Value = new Pos(1, 1, 1);
            grandChild.LocalPos.Value = new Pos(1, 1, 1);

            root.GlobalPos.Value = new Pos(1, 1, 1);

            Assert.AreEqual(new Pos(2, 2, 2), child.GlobalPos.Value);
            Assert.AreEqual(new Pos(3, 3, 3), grandChild.GlobalPos.Value);
        }

    }
}
