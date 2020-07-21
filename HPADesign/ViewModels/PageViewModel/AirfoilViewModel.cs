using HPADesign.Models;
using System;
using Reactive.Bindings;
using System.IO;
using Windows.Storage;
using HPADesign.Services;
using HPADesign.Utilities;
using HPADesign.Helpers;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using HPADesign.Models.Airfoils;

namespace HPADesign.ViewModels
{
    class AirfoilViewModel
    {
        public ReadOnlyReactiveCollection<Airfoil> AirfoilList { get; set; }
        public ReactiveProperty<Airfoil> SelectedAirfoil { get; set; } = new ReactiveProperty<Airfoil>();

        public ReactiveProperty<PointCollection> AirfoilPoints { get; set; } = new ReactiveProperty<PointCollection>();

        //public ReactiveCommand addAirfoil { get; set; }

        public AirfoilViewModel()
        {
            //addAirfoil.Subscribe(_ => { });
            
            SelectedAirfoil.Subscribe(x =>
            {
                if(x==null)
                {
                    return;
                }
                AirfoilPoints.Value = new PointCollection();
                x.Coordinate.NormalPoints.ForEach(point =>
                {
                    AirfoilPoints.Value.Add(new Point(50 + 300 * point.x, 150 - 300 * point.y));
                });
            });
            
            AirfoilList = Project.Airfoil.ToReadOnlyReactiveCollection();
        }
        public async Task AddAirfoil()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".dat");
            //ファイルを開く
            var files = await picker.PickMultipleFilesAsync();
            foreach (StorageFile file in files)
            {
                Stream stream = await file.OpenStreamForReadAsync();
                string name = file.DisplayName;
                AirfoilReader ar = new AirfoilReader(stream,name);
                Airfoil airfoil = ar.Read();

                Project.Airfoil.Add(airfoil);
            }
        }
        
    }
    
}
