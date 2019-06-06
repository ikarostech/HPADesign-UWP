using Prism.Mvvm;

using System.Collections.ObjectModel;
using HPADesign.Models.Component;


namespace HPADesign.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Project : BindableBase
    {
        public Wing Wing { get; set; }

        public ObservableCollection<Airfoil> Airfoils { get; set; }

        public Project()
        {
            Wing = new Wing(this);
            Airfoils = new ObservableCollection<Airfoil>();
        }
    }
}
