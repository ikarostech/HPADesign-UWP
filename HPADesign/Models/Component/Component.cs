using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Component
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
        public virtual ReactiveProperty<Component> Parent { get; set; }
        public virtual ComponentCollection<Component> Children { get; set; }

        public ReactiveProperty<string> Name { get; set; }

        public ReactiveProperty<Pos> GlobalPos { get; set; } = new ReactiveProperty<Pos>(new Pos());
        public ReactiveProperty<Pos> LocalPos { get; set; } = new ReactiveProperty<Pos>(new Pos());

        public ReactiveProperty<double> Mass { get; set; } = new ReactiveProperty<double>(0);
        public Component()
        {
            Parent = new ReactiveProperty<Component>();
            Children = new ComponentCollection<Component>();
            
            GlobalPos.Subscribe( x =>
            {
                if (Parent.Value != null)
                {
                    LocalPos.Value = x - Parent.Value.GlobalPos.Value;
                }
            });

            LocalPos.Subscribe(x =>
           {
               if (Parent.Value != null)
               {
                   GlobalPos.Value = Parent.Value.GlobalPos.Value + x;
               }
           });

            Children.ObserveElementObservableProperty(x => x.Mass)
                .Subscribe(x =>
               {
                   Mass.Value = Children.Sum(y => y.Mass.Value);
               });
        }
    }
    public class ComponentCollection<T> : ReactiveCollection<T>
    {
        public void AddChild(Component parent, Component child)
        {
            parent.Children.Add(child);
            child.Parent = new ReactiveProperty<Component>(parent);
        }
    }

}
