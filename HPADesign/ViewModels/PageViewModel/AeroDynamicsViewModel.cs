using HPADesign.Models;
using HPADesign.Helpers;
using Reactive.Bindings;


namespace HPADesign.ViewModels
{
    /// <summary>
    /// Lift and Drugを編集するところ
    /// </summary>
    public class AeroDynamicsViewModel : Observable,IPageViewModel
    {
        public Project project { get; }

        //private ReadOnlyReactiveCollection<Rib> Ribs {get;set;}
        public ReadOnlyReactiveCollection<RibViewModel> Ribs { get; set; }
        
        
        public AeroDynamicsViewModel(Project project)
        {
            this.project = project;
            //
            Ribs = project.Wing.Ribs.ToReadOnlyReactiveCollection(x => new RibViewModel(x));

            
        }
    }
}
