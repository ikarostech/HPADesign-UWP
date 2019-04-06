using HPADesign.Models;
using System;
using Reactive.Bindings;
using System.IO;
using Windows.Storage;
using HPADesign.Services;
using HPADesign.Utilities;
using Reactive.Bindings;
using HPADesign.Helpers;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace HPADesign.ViewModels
{
    class AirfoilViewModel : Observable, IPageViewModel
    {
        public Project project { get; }

        public ReadOnlyReactiveCollection<Airfoil> AirfoilList { get; set; }
        public ReactiveProperty<Airfoil> SelectedAirfoil { get; set; }

        public ReactiveProperty<PointCollection> AirfoilPoints { get; set; }


        public AirfoilViewModel(Project project)
        {
            this.project = project;
            
            
            AirfoilList = project.Airfoils.ToReadOnlyReactiveCollection(x => x);
            AirfoilPoints = new ReactiveProperty<PointCollection>();
            AirfoilPoints.Value = new PointCollection();
            
        }
        public async Task AddAirfoil()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".dat");
            var files = await picker.PickMultipleFilesAsync();
            foreach (StorageFile file in files)
            {
                Stream stream = await file.OpenStreamForReadAsync();
                AirfoilReader ar = new AirfoilReader(stream);
                Airfoil airfoil = ar.Read();
                project.Airfoils.Add(airfoil);
            }
        }
        public void ChangeSelectedAirfoil()
        {
            //AirfoilPointsの更新を行う
        }
    }
    
}
