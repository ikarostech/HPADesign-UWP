using HPADesign.Models;
using HPADesign.Models.Component;
using HPADesign.Helpers;
using Reactive.Bindings;


namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel : PageViewModel
    {
        

        //private ReadOnlyReactiveCollection<Rib> Ribs {get;set;}
        public ReadOnlyReactiveCollection<RibViewModel> Ribs { get; set; }
        
        
        public AeroDynamicsViewModel(Project project) : base(project)
        {

        }

    }
}
