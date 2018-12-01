using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Project : BindableBase
    {
        public Wing Wing { get; set; }

        public ObservableCollection<Airfoil> Airfoils;

        public Project()
        {
            Wing = new Wing();
            Airfoils = new ObservableCollection<Airfoil>();
        }
    }
}
