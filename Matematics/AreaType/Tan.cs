using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    [Serializable]
    public class Tan
    {
        /// <summary>
        /// Argument of the Tan
        /// </summary>
        public Area Argument { get; set; }

        public Tan()
        {
            Argument = new Area();
        }

        public Tan(Area argument)
        {
            Argument = argument;
        }

        public double TakeNumber()
        {
            return Math.Tan(Argument.Execute());
        }
    }
}
