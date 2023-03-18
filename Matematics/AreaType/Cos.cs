using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    public class Cos
    {
        /// <summary>
        /// Argument of the cos
        /// </summary>
        public Area Argument { get; set; }

        public Cos()
        {
            Argument = new Area();
        }

        public Cos(Area argument)
        {
            Argument = argument;
        }

        public double TakeNumber()
        {
            return Math.Cos(Argument.Execute());
        }
    }
}
