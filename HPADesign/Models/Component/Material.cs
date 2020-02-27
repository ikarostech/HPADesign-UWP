using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Models.Component
{
    public interface IMaterial
    {
        string Name { get; }

        double Density { get; }

    }
    class Material : IMaterial
    {
        public string Name { get; set; }

        public double Density { get; set; }
    }
}
