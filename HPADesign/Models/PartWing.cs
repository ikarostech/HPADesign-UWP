using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    /// <summary>
    /// 翼分割区間
    /// </summary>
    public class PartWing : BindableBase
    {
        private Wing Parent;


        public ObservableCollection<Rib> Ribs { get; set; } = new ObservableCollection<Rib>();
        public int Id { get; set; }

        public int length;
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
                RaisePropertyChanged(nameof(Length));
            }
        }

        //TODO
        private int startpos = 0;
        public int StartPos
        {
            get
            {
                return startpos;
            }
            set
            {
                startpos = value;
                
                RaisePropertyChanged(nameof(StartPos));
                RaisePropertyChanged(nameof(Length));
            }
        }
        //TODO
        //private int endpos;
        public int EndPos {
            get
            {
                return startpos+length;
            }
            set
            {
                length = value-startpos;
                RaisePropertyChanged(nameof(EndPos));
                RaisePropertyChanged(nameof(Length));
            }
        }

        public int Offset { get; set; }

        private int minchord;
        public int MinChord
        {
            get
            {
                return minchord;
            }
            set
            {
                minchord = value;
                RaisePropertyChanged(nameof(MinChord));
                if (autorib)
                {
                    AutoRibsGenerate();
                }

            }
        }

        private int maxchord;
        public int MaxChord
        {
            get
            {
                return maxchord;
            }
            set
            {
                maxchord = value;
                RaisePropertyChanged(nameof(MaxChord));
                if (autorib)
                {
                    AutoRibsGenerate();
                }
            }
        }

        public int Differencial
        {
            get
            {
                return MaxChord - MinChord;
            }
        }

        private bool autorib = true;
        public bool AutoRib
        {
            get
            {
                return autorib;
            }
            set
            {
                autorib = value;
                RaisePropertyChanged(nameof(AutoRib));
            }
        }

        private int ribcount=10;
        public int RibCount
        {
            get
            {
                return ribcount;
            }
            set
            {
                ribcount = value;
                RaisePropertyChanged(nameof(RibCount));

                if (autorib)
                {
                    AutoRibsGenerate();
                }
            }
        }
        private void AutoRibsGenerate()
        {
            Ribs.Clear();
            double interval = Differencial / (RibCount - 1);

            Enumerable.Range(1, RibCount).ToList().ForEach
            (i =>
              Ribs.Add(new Rib(this,(double)MaxChord - interval * (i - 1),(Length)/(RibCount-1)*(i-1)))
            );
            RaisePropertyChanged(nameof(Ribs));
            Parent.PartWingRibsUpdate();
        }

        //TODO
        public void RibUpdate()
        {
            //翼型について考察

            //翼型
            List<int> Foilindex = new List<int>();
            for(int i=0; i<RibCount; i++)
            {
                if(Ribs[i].AirfoilName!=string.Empty)
                {
                    Foilindex.Add(i);
                }
            }
            for(int i=0; i<Foilindex.Count-1; i++)
            {
                for(int j=Foilindex[i]; j<Foilindex[i+1]; j++)
                {
                    double rate = (j - Foilindex[i]) / (Foilindex[i + 1] - Foilindex[i]);
                    Ribs[j].Airfoil = Airfoil.Lerp(Ribs[Foilindex[i]].Airfoil, Ribs[Foilindex[i + 1]].Airfoil, rate);
                }
            }
            for(int i=Foilindex[Foilindex.Count-1]; i<RibCount; i++)
            {
                Ribs[i].Airfoil = Ribs[Foilindex[Foilindex.Count - 1]].Airfoil;
            }
        }
         

        public PartWing(Wing Parent)
        {
            this.Parent = Parent;
            maxchord = 0;
            minchord = 0;
            Ribs = new ObservableCollection<Rib>();
        }
    }
}
