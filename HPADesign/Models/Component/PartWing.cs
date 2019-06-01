using HPADesign.Models.Component;
using Prism.Mvvm;
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

        }

        //TODO
        public void Update()
        {

        }
         

        public PartWing(Project project) : base(project) { }

    }
}
