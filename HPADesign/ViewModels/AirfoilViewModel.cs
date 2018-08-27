using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Drawing;
using Windows.UI.Xaml.Media;
using System.IO;
using Windows.Storage;
using HPADesign.Services;
using HPADesign.Utilities;

namespace HPADesign.ViewModels
{
    class AirfoilViewModel
    {
        public ReactiveCollection<IAirfoil> AirfoilList { get; set; }
        public ReactiveCollection<int> SelectedAirfoilList { get; set; }
        public ReactiveCollection<Windows.Foundation.Point> Points { get; set; }
        public ReactiveCommand AddAirfoil { get; set; }

        public AirfoilViewModel()
        {
            AirfoilList = new ReactiveCollection<IAirfoil>();
            SelectedAirfoilList = new ReactiveCollection<int>();
            Points = new ReactiveCollection<Windows.Foundation.Point>();
            //points.Value = new PointCollection();

            AddAirfoil = new ReactiveCommand();

            AddAirfoil.Subscribe(async (_) =>
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.FileTypeFilter.Add(".dat");
                var files = await picker.PickMultipleFilesAsync();
                foreach(StorageFile file in files)
                {
                    Stream stream = await file.OpenStreamForReadAsync();
                    AirfoilReader ar = new AirfoilReader(stream);
                    Airfoil airfoil = ar.Read();
                    AirfoilList.Add(airfoil);
                }

            });


            //以下テストコード
            SelectedAirfoilList.Add(2);

            Points.Add(new Windows.Foundation.Point(10, 10));
            Points.Add(new Windows.Foundation.Point(1000, 1000));
        }
    }
}
