using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Components
{
    public class Propeller : Component
    {
        private Propeller()
        {
            
        }
        public Propeller(Plane parent) : this()
        {
            parent.Propeller.Value = this;
        }
       //public Propeller(Project project) : base(project) { }
    }
}
