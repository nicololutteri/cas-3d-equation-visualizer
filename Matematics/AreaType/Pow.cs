using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    /// <summary>
    /// Area ^ Exponential
    /// </summary>
    [Serializable]
    public class Pow
    {
        /// <summary>
        /// The base of the pow
        /// </summary>
        public Area Area { get; set; }
        /// <summary>
        /// The exponental of the pow
        /// Area ^ exponential
        /// </summary>
        public int Exponential { get; set; }

        public Pow()
        {
            Area = new Area();
        }

        public Pow(Area area, int exponental)
        {
            Area = area;
            Exponential = exponental;
        }

        public double TakeNumber()
        {
            return Math.Pow(Area.Execute(), Exponential);
        }
    }
}
