using Matematics.Basic;
using Matematics.Other;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Matematics.Graphic
{
    public class Graphic
    {
        /// <summary>
        /// The start of the X and Y
        /// </summary>
        public int Numbermin { get; set; }
        /// <summary>
        /// The finish of the X and Y
        /// </summary>
        public int Numbermax { get; set; }
        /// <summary>
        /// The precision
        /// (2 means 0,01)
        /// </summary>
        public double Precision { get; set; }
        /// <summary>
        /// List of all the coordinates
        /// </summary>
        public List<Coordinates> ListCoordinates { get; set; }

        /// <summary>
        /// Indicates the scale about X
        /// </summary>
        public int ScaleX { get; set; }
        /// <summary>
        /// Indicates the scale about Y
        /// </summary>
        public int ScaleY { get; set; }
        /// <summary>
        /// Indicates the scale about Z
        /// </summary>
        public int ScaleZ { get; set; }

        public int GraduationMultiple { get; set; }

        /// <summary>
        /// The equation you would the graphics
        /// </summary>
        public Area Equation { get; set; }

        /// <summary>
        /// Indicates if the function is Tridimensional (with X, Y, Z)
        /// </summary>
        public bool Tridimensional { get; set; }

        public Graphic()
        {
            ListCoordinates = new List<Coordinates>();
            Equation = new Area();
        }

        public Graphic(int numbermin, int numbermax, int precision, Area equation)
        {
            Numbermin = numbermin;
            Numbermax = numbermax;
            Precision = precision;

            ListCoordinates = new List<Coordinates>();
            Equation = equation;
        }

        public void Prepare()
        {
            if (Equation.MType != Other.Type.Equation)
            {
                return;
            }

            Delete();

            for (int x = Numbermin; x <= Numbermax; x++)
            {
                Area tmp = Utilities.Basic.DeepCopy<Area>(Equation);
                Area forreplace = new(false, new Monomio(Utilities.NumberManipulation.Value(x, Precision)), Other.Type.Monomio);

                tmp.Replace(new Basic.Letter("x", 1), forreplace);

                if (Tridimensional)
                {
                    for (int y = Numbermin; y <= Numbermax; y++)
                    {
                        Area tmp2 = Utilities.Basic.DeepCopy<Area>(tmp);
                        Area forreplacey = new(false, new Monomio(Utilities.NumberManipulation.Value(y, Precision)), Other.Type.Monomio);
                        tmp2.Replace(new Basic.Letter("y", 1), forreplacey);
                        ListCoordinates.Add(new Coordinates(Convert.ToDouble(x), Convert.ToDouble(y), tmp2.Execute()));
                    }
                }
                else
                {
                    ListCoordinates.Add(new Coordinates(Convert.ToDouble(x), tmp.Execute()));
                }
            }
        }

        public Point[] ToVector()
        {
            Point[] list = new Point[ListCoordinates.Count];
            for (int x = 0; x < ListCoordinates.Count; x++)
            {
                list[x] = Transform(ListCoordinates[x].X, ListCoordinates[x].Y);
            }

            return list;
        }

        public Point Transform(double x, double y)
        {
            int xInteger = (int)((x) * ScaleX);
            int yInteger = (int)((y) * ScaleY);

            return new Point(xInteger, yInteger);
        }

        public void Delete()
        {
            ListCoordinates.Clear();
        }
    }
}
