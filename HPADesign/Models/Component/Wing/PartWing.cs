using HPADesign.Models.Component;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace HPADesign.Models.Component
{
    /// <summary>
    /// 翼分割区間
    /// </summary>
    public class PartWing : Component
    {

        public int Id { get; set; }


        public int length;
        public　ReactiveProperty<int> Length { get; set; }
        //TODO
        public ReactiveProperty<Pos> StartPos { get; set; }
        //TODO
        //private int endpos;
        public ReactiveProperty<Pos> EndPos { get; set; }


        public ReactiveProperty<int> Offset { get; set; }

        public ReactiveProperty<int> MinChord { get; set; }
        
        public ReactiveProperty<int> MaxChord { get; set; }
        
        public ReadOnlyReactiveProperty<int> DifferentialChord { get; set; }

        //public ReactiveProperty<bool> AutoRib = true;

        public ReactiveProperty<int> RibCount { get; set; }

        

        private void AutoRibsGenerate()
        {
            for (int i = 0; i < RibCount.Value; i++)
            {
                Ribs.Clear();
                var wr = new WingRib();
                wr.Chord = 1200;
                Ribs.AddOnScheduler(wr);
            }
        }

        //TODO
        public ReactiveCollection<Rib> Ribs { get; set; }
        

        public PartWing()
        {
            Ribs = new ReactiveCollection<Rib>();
            AutoRibsGenerate();
            //Ribs.Add(new WingRib());
            
        }

    }
}
