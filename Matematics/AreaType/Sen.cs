using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    [Serializable]
    public class Sen
    {
        /// <summary>
        /// Argument of the Sen
        /// </summary>
        public Area Argument { get; set; }

        public Sen()
        {
            Argument = new Area();
        }

        public Sen(Area argument)
        {
            Argument = argument;
        }

        public double TakeNumber()
        {
            return Math.Sin(Argument.Execute());
        }
    }
}
