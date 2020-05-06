using HPADesign.Models.Component;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace HPADesign.Models.Component
{
    /// <summary>
    /// 翼分割区間
    /// </summary>
    public class PartWing : Component
    {

        public int Id { get; set; }



        public ReactiveProperty<int> Length { get; set; } = new ReactiveProperty<int>();
        //TODO
        public ReactiveProperty<Pos> StartPos { get; set; }
        //TODO
        //private int endpos;
        public ReactiveProperty<Pos> EndPos { get; set; }

        public ReactiveProperty<bool> AutoRib { get; set; } = new ReactiveProperty<bool>(true);

        public ReactiveProperty<int> Offset { get; set; } = new ReactiveProperty<int>();

        public ReactiveProperty<int> MinChord { get; set; } = new ReactiveProperty<int>();

        public ReactiveProperty<int> MaxChord { get; set; } = new ReactiveProperty<int>();
        
        public ReactiveProperty<int> DifferentialChord { get; private set; }

        //public ReactiveProperty<bool> AutoRib = true;

        public ReactiveProperty<int> RibCount { get; set; } = new ReactiveProperty<int>(10);

        

        private void AutoRibsGenerate()
        {
            Ribs.Clear();
            for (int i = 0; i < RibCount.Value; i++)
            {
                var wr = new Rib();
                wr.Chord.Value = 1200;
                
                Ribs.Add(wr);
            }
        }

        //TODO
        public ObservableCollection<Rib> Ribs { get; set; }
        

        public PartWing()
        {
            Ribs = new ReactiveCollection<Rib>();

            MaxChord.Select(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());
            MinChord.Select(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());
            RibCount.Select(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());

            LocalPos.Subscribe(x =>
            {
                if (StartPos != null)
                {
                    StartPos.Value = x;
                }
            });

            DifferentialChord = MaxChord.CombineLatest(MinChord, (x, y) => x - y).ToReactiveProperty();

            
            //Ribs.Add(new WingRib());

        }

    }
}
