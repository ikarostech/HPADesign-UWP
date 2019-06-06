using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Component
{
    public interface IComponent
    {
        Component Parent { get; set; }
        ObservableCollection<Component> Children { get; set; }

        

        Pos GlobalPos { get; set; }
        Pos LocalPos { get; set; }

        double Mass { get; set; }
    }
    public interface IElement
    {
        double Name { get; }
        double Volume { get; set; }
        double Mass { get; }

        IMaterial Material { get; set; }
    }

    public interface IMaterial
    {
        double Name { get; }

        double Density { get; }

    }
    public class Component : BindableBase, IComponent
    {
        public Component Parent { get; set; }
        public ObservableCollection<Component> Children { get; set; }

        private Project Project { get; }

        private Pos globalpos;
        public Pos GlobalPos
        {
            get
            {
                return globalpos;
            }
            set
            {
                globalpos = value;
                if (Parent == null)
                {
                    localpos = value;
                }
                else
                {
                    localpos = globalpos - Parent.GlobalPos;
                }
                RaisePropertyChanged(nameof(LocalPos));
                RaisePropertyChanged(nameof(GlobalPos));
            }
        }

        private Pos localpos;
        public Pos LocalPos
        {
            get
            {
                return localpos;
            }
            set
            {
                localpos = value;
                if (Parent == null)
                {
                    globalpos = value;
                }
                else
                {
                    globalpos = Parent.GlobalPos + localpos;
                }
                RaisePropertyChanged(nameof(LocalPos));
                RaisePropertyChanged(nameof(GlobalPos));
            }
        }

        public double Mass
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public Component(Project project)
        {
            Project = project;
            Children = new ObservableCollection<Component>();
        }
    }
}
