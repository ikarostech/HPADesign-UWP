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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Length"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinChord"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxChord"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AutoRib"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RibCount"));

                if (autorib)
                {
                    AutoRibsGenerate();
                }
            }
        }
        private void AutoRibsGenerate()
        {
            Ribs.Clear();
            double interval = (double)Differencial / (RibCount - 1);

            Enumerable.Range(1, RibCount).ToList().ForEach
            (i =>
              Ribs.Add(new Rib(interval * (i - 1)))
            );
            RaisePropertyChanged(nameof(Ribs));
            Parent.PartWingUpdate();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public PartWing(Wing Parent)
        {
            this.Parent = Parent;
            maxchord = 0;
            minchord = 0;
            Ribs = new ObservableCollection<Rib>();
        }
    }
}
