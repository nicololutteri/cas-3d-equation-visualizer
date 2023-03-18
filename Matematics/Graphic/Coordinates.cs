using System;
using System.Drawing;

namespace Matematics.Graphic
{
    [Serializable]
    public class Coordinates
    {
        /// <summary>
        /// The x coordinates
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The Y coordinates
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The Z coordinates
        /// </summary>
        public double Z { get; set; }

        public Coordinates()
        {

        }

        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return "{" + "X: " + X.ToString() + ", " + "Y: " + Y.ToString() + ", " + "Z: " + Z.ToString() + "}";
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }
}
