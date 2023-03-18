using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    [Obsolete]
    public class Parenteshis
    {
        /// <summary>
        /// The argumet of the parenteshis
        /// </summary>
        public Area Argument { get; set; }

        public Parenteshis()
        {

        }

        public Parenteshis(Area argument)
        {
            Argument = argument;
        }
    }
}
