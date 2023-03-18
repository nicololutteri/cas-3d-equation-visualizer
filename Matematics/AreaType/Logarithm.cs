using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    [Serializable]
    public class Logarithm
    {
        /// <summary>
        /// Base of the Logarithm
        /// </summary>
        public double BaseNumber { get; set; }
        /// <summary>
        /// Argument of the Logharithm (Area)
        /// </summary>
        public Area Argument { get; set; }

        public Logarithm()
        {
            Argument = new Area();
        }

        public Logarithm(double basenumber, Area argument)
        {
            BaseNumber = basenumber;
            Argument = argument;
        }

        public double TakeNumber()
        {
            return Math.Log(Argument.Execute(), BaseNumber);
        }
    }
}
