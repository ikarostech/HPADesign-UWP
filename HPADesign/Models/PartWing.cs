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

        public int MinChord { get; set; } = 0;
        public int MaxChord { get; set; } = 0;

        public int RibCount { get; set; } = 10;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
