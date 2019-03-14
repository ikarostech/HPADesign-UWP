using Prism.Mvvm;

using System.Collections.ObjectModel;


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

            Airfoil a1 = new Airfoil();
            a1.Name = "test1";
            Airfoil a2 = new Airfoil();
            a2.Name = "test2";

            Airfoils.Add(a1);
            Airfoils.Add(a2);
        }
    }
}
