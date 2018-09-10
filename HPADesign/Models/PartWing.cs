using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models
{
    /// <summary>
    /// 翼分割区間
    /// </summary>
    public class PartWing
    {
        public List<Rib> Ribs { get; set; }
        public int Id { get; set; }
        public int Length { get; set; } = 3000;

        public int Offset { get; set; }

        public int MinChord { get; set; } = 0;
        public int MaxChord { get; set; } = 0;

        public int RibCount { get; set; } = 10;
    }
}
