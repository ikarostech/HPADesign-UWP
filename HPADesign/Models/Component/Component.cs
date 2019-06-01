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
        IComponent Parent { get; set; }
        ObservableCollection<IComponent> Children { get; set; }

        Project Project { get; }

        int GlobalPos { get; set; }
        int LocalPos { get; set; }

        double Mass { get; set; }
    }

    public interface IElement
    {

    }

    public class Component : BindableBase, IComponent
    {
        public IComponent Parent { get; set; }
        public ObservableCollection<IComponent> Children { get; set; }

        public Project Project { get; }

        private int globalpos;
        public int GlobalPos
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

        private int localpos;
        public int LocalPos
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
            Children = new ObservableCollection<IComponent>();
        }
    }
}
