using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace HPADesign.Models.Components
{
    public interface IComponent
    {
        //ReactiveProperty<Component> Parent { get; set; }
        //ObservableCollection<Component> Children { get; set; }

        ReactiveProperty<string> Name { get; set; }

        ReactiveProperty<Pos> GlobalPos { get; set; } 
        ReactiveProperty<Pos> LocalPos { get; set; } 

        ReactiveProperty<double> Mass { get; set; }
    }
    public interface IElement : IComponent
    {
        new ReactiveProperty<double> Mass { get; set; }

        ReactiveProperty<IMaterial> Material { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class Component : IComponent
    {

        public virtual ReactiveProperty<Component> Parent { get; set; } = new ReactiveProperty<Component>();
        public virtual ReactiveCollection<Component> Children { get; set; } = new ReactiveCollection<Component>();

        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>();

        public ReactiveProperty<Pos> GlobalPos { get; set; } = new ReactiveProperty<Pos>(new Pos());
        public ReactiveProperty<Pos> LocalPos { get; set; } = new ReactiveProperty<Pos>(new Pos());

        public ReactiveProperty<double> Mass { get; set; } = new ReactiveProperty<double>(0);

        protected void BindProperty<T>(ReactiveProperty<T> Property)
            where T : Component
        {
            Property.Subscribe(x =>
            {
                if(x != null)
                {
                    //xにParentを設定
                    x.Parent.Value = this;
                    Children.Add(x);
                }
                
                T old = (T)Children.Where(u => u is T).FirstOrDefault();
                
                if (old != null && old != x)
                {
                    Children.Remove(old);
                }
            });
        }

        protected void BindCollection<T>(ReactiveCollection<T> reactiveCollection)
            where T : Component
        {
            reactiveCollection.ObserveAddChanged().Subscribe(x =>
            {
                x.Parent.Value = this;
                Children.Add(x);
            });
            reactiveCollection.ObserveRemoveChanged().Subscribe(x =>
            {
                Children.Remove(x);
            });
        }

        public Component(ReactiveProperty<Component> property) : this()
        {
            //ChildrenのBindPropertyを発火させる
            property.Value = this;
        }
        //Addに対応
        public Component(ReactiveCollection<Component> collection) : this()
        {
            collection.Add(this);
        }
        //Insertに対応
        public Component(ReactiveCollection<Component> collection, int index) : this()
        {
            collection.Insert(index, this);
        }
        protected Component() {
            GlobalPos.Subscribe(x =>
            {
                if (Parent.Value != null)
                {
                    LocalPos.Value = x - Parent.Value.GlobalPos.Value;
                }
                else
                {
                    LocalPos.Value = x;
                }
                foreach(Component child in Children)
                {
                    child.GlobalPos.Value = x + child.LocalPos.Value;
                }
            });

            LocalPos.Subscribe(x =>
            {
                if (Parent.Value != null)
                {
                    GlobalPos.Value = Parent.Value.GlobalPos.Value + x;
                }
                else
                {
                    GlobalPos.Value = x;
                }
            });

            
        }
    }
}
