using Matematics.Basic;
using Matematics.Other;
using System;
using System.Collections.Generic;

namespace Matematics.AreaType
{
    [Serializable]
    public class Fraction
    {
        /// <summary>
        /// The numerator of the fraction
        /// </summary>
        public Area Numerator { get; set; }
        /// <summary>
        /// The denominator of the denominator
        /// </summary>
        public Area Denominator { get; set; }

        public Fraction()
        {
            Numerator = new Area();
            Denominator = new Area();
        }

        public Fraction(Area numerator, Area denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public void Simplify()
        {
            Numerator.Simplify(null);
            Denominator.Simplify(null);
        }

        public Area Sum(Fraction fractionsum)
        {
            //7/2 + 6/4 = (4/2*7) + (4/4*6)/4

            Fraction tmp = new();
            List<Area> list = new()
            {
                tmp.Denominator,
                fractionsum.Denominator
            };
            tmp.Denominator = MCMAndMCD.FindMCM(list);

            ListOfArea num = new();
            num.Area.Add(new Area(false, new Fraction(tmp.Denominator, Numerator), Other.Type.Fraction));
            num.Area.Add(new Area(false, new Multiply(num.Area), Other.Type.Multiply));

            Numerator = tmp.Numerator;
            Denominator = tmp.Denominator;
            return new Area(false, tmp, Other.Type.Fraction);
        }

        public void Replace(Letter x, Area a)
        {
            Numerator.Replace(x, a);
            Denominator.Replace(x, a);
        }

        public double TakeANumber()
        {
            double n = Numerator.Execute();
            double d = Denominator.Execute();

            if (n % d != 0)
            {
                throw new Exception.NumerIsNotIntegerException();
            }
            else
            {
                return (n / d);
            }
        }
    }
}
