using HPADesign.Models.Components;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace HPADesign.Models.Components.Wings
{
    /// <summary>
    /// 翼分割区間
    /// </summary>
    public class WingSection : Component
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

        

        public void AutoRibsGenerate()
        {

            Children.Where(x => x is Rib).ToList().All(i => Children.Remove(i));
            for (int i = 0; i < RibCount.Value; i++)
            {
                var wr = new Rib(this);
                wr.Parent.Value = this;
                wr.Chord.Value = MaxChord.Value - (MaxChord.Value - MinChord.Value) * i / (RibCount.Value -1);
                wr.LocalPos.Value.x = Length.Value * i / (RibCount.Value - 1);              
            }
        }

        //TODO
        public ReactiveCollection<Rib> Ribs { get; private set; } = new ReactiveCollection<Rib>();

        public ReactiveProperty<Plank> Plank { get; set; } = new ReactiveProperty<Plank>();
        public ReactiveCollection<Stringer> Stringers { get; set; } = new ReactiveCollection<Stringer>();
        public ReactiveProperty<TrainingEdge> TrainingEdge { get; set; } = new ReactiveProperty<TrainingEdge>();
        

        private WingSection()
        {
            BindProperty(Plank);
            BindProperty(TrainingEdge);
            BindCollection(Ribs);

            AutoRib.Value = false;
            MaxChord.Where(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());
            MinChord.Where(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());
            RibCount.Where(_ => AutoRib.Value).Subscribe(_ => AutoRibsGenerate());
            AutoRib.Value = true;

            DifferentialChord = MaxChord.CombineLatest(MinChord, (x, y) => x - y).ToReactiveProperty();
        }
        public WingSection(Wing parent)
        {
            if (parent != null)
            {
                parent.PartWings.Add(this);
            }
        }
        public WingSection(Wing parent, int index)
        {
            if (parent != null)
            {
                parent.PartWings.Insert(index, this);
            }
        }

    }
}
