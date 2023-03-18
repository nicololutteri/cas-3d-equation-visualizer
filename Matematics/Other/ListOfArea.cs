using Matematics.Basic;
using System;
using System.Collections.Generic;

namespace Matematics.Other
{
    [Serializable]
    public class ListOfArea
    {
        /// <summary>
        /// The list of the area
        /// </summary>
        public List<Area> Area { get; set; }

        public ListOfArea()
        {
            Area = new List<Area>();
        }

        public ListOfArea(List<Area> area)
        {
            Area = area;
        }

        public void Replace(Letter x, Area area)
        {
            foreach (Area j in Area)
            {
                j.Replace(x, area);
            }
        }
    }
}
