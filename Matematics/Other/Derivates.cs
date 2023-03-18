using Matematics.AreaType;
using Matematics.Basic;
using Matematics.Other;
using System;
using System.Collections.Generic;
using Utilities;

namespace Matematics
{
    public class Derivates
    {
        /// <summary>
        /// The variable to convert
        /// </summary>
        public Char VarToConvert { get; set; }

        public Derivates()
        {

        }

        public Derivates(Char varToConvert)
        {
            VarToConvert = varToConvert;
        }

        public Monomio ToDerivate(Monomio x)
        {
            // x^1 = 1
            // y^1 = 1

            // e * x^n = e * n * x^ (n - 1) 

            List<Letter> chartmp = new();
            Monomio tmp = new(x.Number);

            foreach (Letter j in x.Char)
            {
                if (j.IsEqualsChar(new Letter(VarToConvert, 1)))
                {
                    chartmp.Add(new Letter(j.Char, j.Grade - 1));
                    tmp.Number = tmp.Number * j.Grade;
                }
                else
                {
                    chartmp.Add(new Letter(j.Char, j.Grade));
                }
            }

            tmp.Char = chartmp;
            tmp.Normalize();

            return tmp;
        }

        /// <summary>
        /// Derivate of an area
        /// </summary>
        /// <param name="x">Area input</param>
        /// <returns>Final result</returns>
        public Area ToDerivateArea(Area x)
        {
            switch (x.MType)
            {
                case Other.Type.Polynomial:
                    Polynomial tmpp = new();
                    foreach (Monomio j in ((Polynomial)x.Space).Monomio)
                    {
                        tmpp.Monomio.Add(ToDerivate(j));
                    }

                    return new Area(false, tmpp, Other.Type.Polynomial);
                case Other.Type.Sqrt:

                case Other.Type.ListOfArea:
                    ListOfArea tmplist = new();
                    foreach (Area j in ((ListOfArea)x.Space).Area)
                    {
                        tmplist.Area.Add(ToDerivateArea(j));
                    }

                    return new Area(false, tmplist, Other.Type.ListOfArea);
                case Other.Type.Cos:
                    Multiply mc = new();
                    Sen sen = new(x);
                    Area sin = new(Operations.MultiplicationSign(true, x.Sign), sen, Other.Type.Sen);

                    mc.Add(sin);
                    mc.Add(ToDerivateArea(x));

                    return new Area(false, mc, Other.Type.Multiply);
                case Other.Type.Sen:
                    Multiply mcs = new();
                    Cos cos = new(x);
                    Area costmp = new(Operations.MultiplicationSign(false, x.Sign), cos, Other.Type.Cos);

                    mcs.Add(costmp);
                    mcs.Add(ToDerivateArea(x));

                    return new Area(false, mcs, Other.Type.Multiply);
                case Other.Type.Tan:
                    Fraction fn = new();

                    Polynomial pa = new();
                    pa.Monomio.Add(new Monomio(1));

                    fn.Numerator = new Area(false, pa, Other.Type.Polynomial);

                    Cos cc = new();
                    cc.Argument = x;

                    Pow d = new(new Area(false, cc, Other.Type.Cos), 2);
                    return new Area(false, d, Other.Type.Pow);
                case Other.Type.Monomio:
                    Monomio t = (Monomio)x.Space;
                    return new Area(false, ToDerivate(t), Other.Type.Monomio);
                case Other.Type.Fraction:
                    Fraction f = new();
                    Fraction toconvert = (Fraction)x.Space;

                    ListOfArea la = new();
                    Multiply m = new();
                    m.Add(toconvert.Denominator);
                    m.Add(ToDerivateArea(toconvert.Numerator));

                    la.Area.Add(new Area(false, m, Other.Type.Multiply));

                    Multiply m2 = new();
                    m2.Add(toconvert.Numerator);
                    m2.Add(ToDerivateArea(toconvert.Denominator));

                    la.Area.Add(new Area(true, m, Other.Type.Multiply));

                    f.Numerator = new Area(false, la, Other.Type.ListOfArea);

                    Pow p = new();
                    p.Exponential = 2;
                    p.Area = f.Denominator;

                    f.Denominator = new Area(false, p, Other.Type.Pow);

                    return new Area(false, f, Other.Type.Fraction);
                case Other.Type.Multiply:
                    ListOfArea list = new();

                    Multiply ini = new();
                    ini.Add(ToDerivateArea(((Multiply)x.Space).ListOfArea[0]));
                    ini.Add(((Multiply)x.Space).ListOfArea[1]);
                    list.Area.Add(new Area(false, ini, Other.Type.ListOfArea));

                    Multiply ini2 = new();
                    ini2.Add(((Multiply)x.Space).ListOfArea[0]);
                    ini2.Add(ToDerivateArea(((Multiply)x.Space).ListOfArea[1]));
                    list.Area.Add(new Area(false, ini2, Other.Type.ListOfArea));

                    return new Area(false, list, Other.Type.ListOfArea);
                case Other.Type.Pow:
                    Multiply mtmp = new();

                    Monomio ttm = new(((Pow)x.Space).Exponential - 1);
                    mtmp.Add(new Area(false, ttm, Other.Type.Monomio));

                    Pow dd = new(x, ((Pow)x.Space).Exponential - 1);
                    mtmp.Add(new Area(false, dd, Other.Type.Pow));

                    return new Area(false, mtmp, Other.Type.Multiply);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
