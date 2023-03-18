using Matematics.Graphic;
using System.Collections.Generic;

namespace Sito.Controllers
{
    /// <summary>
    /// The class for the Three grapich
    /// </summary>
    public class ThreeCoordinate
    {
        /// <summary>
        /// The X coordinate
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The Y coordinate
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The Z coordinate
        /// </summary>
        public double Z { get; set; }

        public ThreeCoordinate()
        {

        }

        public ThreeCoordinate(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class ThreeConvert
    {
        public static List<ThreeCoordinate> ConvertToThree(List<Coordinates> list)
        {
            List<ThreeCoordinate> tmp = new();

            foreach (Coordinates x in list)
            {
                tmp.Add(new ThreeCoordinate(x.X, x.Y, x.Z));
            }

            return tmp;
        }

        public static string ConvertToThree(Coordinates[] array, string splitobject, string splitpoint)
        {
            string tmp = "";

            foreach (Coordinates x in array)
            {
                string tmp2 = x.X + splitpoint + x.Y + splitpoint + x.Z;
                tmp += splitobject + tmp2;
            }

            if (tmp.Length != 0)
            {
                tmp = tmp.Remove(0, 1);
            }

            return tmp;
        }
    }
}
