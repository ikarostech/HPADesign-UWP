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
    public class PartWing : INotifyPropertyChanged
    {
        public ObservableCollection<Rib> Ribs { get; set; }
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
                minchord = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinChord"));
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
                    Ribs.Clear();
                    int interval = Differencial / (RibCount - 1);

                    Enumerable.Range(1, RibCount).ToList().ForEach
                    (i =>
                      Ribs.Add(new Rib(interval * (i - 1)))                        
                    );
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
