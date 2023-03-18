using Matematics.Other;
using System;
using System.Collections.Generic;
using Utilities;

namespace Matematics.Basic
{
    [Serializable]
    public class Multiply
    {
        /// <summary>
        /// The list of area of the multiply
        /// </summary>
        private List<Area> listofarea;
        public List<Area> ListOfArea
        {
            get
            {
                if (listofarea.Count != 0)
                {
                    return listofarea;
                }
                else
                {
                    List<Area> tmp = new()
                    {
                        new Area(false, new Monomio(0), Other.Type.Monomio)
                    };
                    return tmp;
                }
            }
            set
            {
                listofarea = value;
            }
        }

        public Multiply()
        {
            ListOfArea = new List<Area>();
        }

        public Multiply(List<Area> listofarea)
        {
            ListOfArea = listofarea;
        }

        public Multiply(Area a1)
        {
            ListOfArea = new List<Area>();
            Add(a1);
        }

        public Multiply(Area a1, Area a2)
        {
            ListOfArea = new List<Area>();
            Add(a1);
            Add(a2);
        }

        public Multiply(Area a1, Area a2, Area a3)
        {
            ListOfArea = new List<Area>();
            Add(a1);
            Add(a2);
            Add(a3);
        }

        public void Add(Area a)
        {
            listofarea.Add(a);
        }
        public int RealDimension()
        {
            return listofarea.Count;
        }

        public double TakeNumber()
        {
            double tmp = 1;
            //false = +
            //true = -
            bool sign = false;

            foreach (Area x in ListOfArea)
            {
                sign = Operations.MultiplicationSign(sign, x.Sign);
                tmp *= x.Execute();
            }

            if (sign == true)
            {
                return -tmp;
            }
            else
            {
                return +tmp;
            }
        }

        public void Replace(Letter x, Area a)
        {
            foreach (Area j in ListOfArea)
            {
                j.Replace(x, a);
            }
        }
    }
}
