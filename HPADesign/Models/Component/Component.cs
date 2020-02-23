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
        ReactiveProperty<Component> Parent { get; set; }
        ReactiveCollection<Component> Children { get; set; }



        ReactiveProperty<Pos> GlobalPos { get; set; }
        ReactiveProperty<Pos> LocalPos { get; set; }

        ReactiveProperty<double> Mass { get; set; }
    }
    public interface IElement
    {
        ReactiveProperty<double> Name { get; }
        ReactiveProperty<double> Volume { get; set; }
        ReactiveProperty<double> Mass { get; }

        ReactiveProperty<IMaterial> Material { get; set; }
    }

    public interface IMaterial
    {
        double Name { get; }

        double Density { get; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class Component : IComponent
    {
        public ReactiveProperty<Component> Parent { get; set; }
        public ReactiveCollection<Component> Children { get; set; }

        public ReactiveProperty<Pos> GlobalPos { get; set; }
        public ReactiveProperty<Pos> LocalPos { get; set; }

        public ReactiveProperty<double> Mass { get; set; }
        public Component()
        {
            //Parent = new ReactiveProperty<Component>();
            Children = new ReactiveCollection<Component>();

            
            GlobalPos.Subscribe( x =>
            {
                LocalPos.Value = x - Parent.Value.GlobalPos.Value;
            });

            LocalPos.Subscribe(x =>
           {
               GlobalPos.Value = Parent.Value.GlobalPos.Value + x;
           });

            Children.ObserveElementObservableProperty(x => x.Mass)
                .Subscribe(x =>
               {
                   Mass.Value = Children.Sum(y => y.Mass.Value);
               });
        }
    }
}
