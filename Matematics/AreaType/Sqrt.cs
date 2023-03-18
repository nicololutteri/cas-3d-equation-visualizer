using Matematics.Basic;
using Matematics.Other;
using System;

namespace Matematics.AreaType
{
    [Serializable]
    public class Sqrt
    {
        /// <summary>
        /// Grade of the Sqrt
        /// (2, 3, 4, ...)
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// The argument of the sqrt
        /// </summary>
        public Area Area { get; set; }

        public Sqrt()
        {
            Area = new Area();
        }

        public Sqrt(int grade, Area area)
        {
            Grade = grade;
            Area = area;
        }

        public Polynomial Execute()
        {
            Polynomial tmp = new();

            if (Area.MType == Other.Type.Polynomial)
            {
                foreach (Monomio x in ((Polynomial)Area.Space).Monomio)
                {
                    tmp.Monomio.Add(x.ExcecuteSqrt(Grade));
                }

                return tmp;
            }

            throw new Exception.CannotResolveException();
        }

        public double ExecuteValue()
        {
            return Execute().Execute();
        }
    }
}
