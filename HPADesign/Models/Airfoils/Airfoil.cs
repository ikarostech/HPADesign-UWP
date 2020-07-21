using System;
using System.Collections.Generic;
using System.Linq;
using HPADesign.Helpers;
using HPADesign.Models.Shape;
using Reactive.Bindings;

namespace HPADesign.Models.Airfoils
{
    public enum AirfoilSide { Upper, Downer }
    public enum AirfoilType { Selig, Lednicer, Null }
    public interface IAirfoil
    {
        ReactiveProperty<string> Name { get; set; }
    }

    public class AirfoilPerformance : AerodynamicsPerformance
    {

    }
    public interface IAirfoilPerformance
    {
        
    }

    public class Airfoil : IAirfoil
    {
        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>("None");

        public AirfoilCoordinate Coordinate { get; set; } = new SeligCoordinate();
        public AirfoilPerformance AirfoilPerformance { get; set; } = new AirfoilPerformance();

        
    }


}

