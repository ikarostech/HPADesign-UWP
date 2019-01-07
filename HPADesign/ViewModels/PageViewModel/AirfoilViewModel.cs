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

namespace HPADesign.ViewModels
{
    class AirfoilViewModel : Observable, IPageViewModel
    {
        public Project project { get; }

        public ReadOnlyReactiveCollection<Airfoil> AirfoilList { get; set; }
        public ReactiveCollection<int> SelectedAirfoilList { get; set; }
        

        public AirfoilViewModel(Project project)
        {
            this.project = project;
            
            SelectedAirfoilList = new ReactiveCollection<int>();
            AirfoilList = project.Airfoils.ToReadOnlyReactiveCollection(x => x);
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
    }
    
}
