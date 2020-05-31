using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Models.Component;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using HPADesign.Helpers;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace HPADesign.ViewModels.ComponentViewModel
{
    public class RibViewModel
    {
        public RibViewModel(Rib rib)
        {
            model = rib;
            rib.Airfoil = new Airfoil();
            Chord = rib.Chord.ToReactiveProperty();
            Lrho = rib.Chord.CombineLatest(rib.Airfoil.AirfoilPerformance.CL, (x, y) => x * y).ToReactiveProperty();
            
            Airfoil = ReactiveProperty.FromObject(rib, x => x.Airfoil);
            Airfoils = Project.Airfoil.ToReadOnlyReactiveCollection();
        }
        private Rib model;
        public ReactiveProperty<double> GlobalPos { get;set; }
        public ReactiveProperty<double> Chord { get; set; }
        public ReactiveProperty<Airfoil> Airfoil { get; set; }
        
        //public ReactiveProperty<string> AirfoilName { get; set; }

        public ReactiveProperty<double> Lrho { get; set; }
        public ReadOnlyReactiveCollection<Airfoil> Airfoils { get; set; }

        public async Task ExportDXF()
        {
            // TODO
            // 本来ここは入らない
            PartWing partWing = model.Parent.Value as PartWing;

            Plank plank = new Plank();
            plank.PlankThin.Value = 2;
            plank.PlankUpperPos.Value = 0.4;
            plank.PlankDownerPos.Value = 0.2;

            Stringer stringer1 = new Stringer();
            stringer1.AirfoilSide.Value = AirfoilSide.Upper;
            stringer1.StringerPos.Value = 0.4;
            stringer1.StringerHeight.Value = 4;
            stringer1.StringerWidth.Value = 2;

            Stringer stringer2 = new Stringer();
            stringer2.AirfoilSide.Value = AirfoilSide.Upper;
            stringer2.StringerPos.Value = 0.2;
            stringer2.StringerHeight.Value = 4;
            stringer2.StringerWidth.Value = 2;

            Stringer stringer3 = new Stringer();
            stringer3.AirfoilSide.Value = AirfoilSide.Downer;
            stringer3.StringerPos.Value = 0.2;
            stringer3.StringerHeight.Value = 4;
            stringer3.StringerWidth.Value = 2;

            partWing.Plank = plank;
            partWing.Stringers.Add(stringer1);
            partWing.Stringers.Add(stringer2);
            partWing.Stringers.Add(stringer3);
            partWing.TrainingEdge.TrainlingEdgeLength.Value = 25;

            //ここまで


            string content = DXF.Content(model);
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("DXF図面交換ファイル", new List<string>() { ".dxf" });
            StorageFile file = await savePicker.PickSaveFileAsync();
            if(file != null)
            {
                await FileIO.WriteTextAsync(file, content);
            }
        }
    }
}
