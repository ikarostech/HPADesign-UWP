using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Models.Airfoils;

namespace HPADesign.Utilities
{
    interface IRibGenerator
    {
        void RegisterAirfoil(IAirfoil airfoil);
    }

    class WingRibGenerator : IRibGenerator
    {
        IAirfoil Airfoil;
        public void RegisterAirfoil(IAirfoil airfoil)
        {
            Airfoil = airfoil;
        }

    }
}
