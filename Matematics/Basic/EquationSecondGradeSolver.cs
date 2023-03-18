using Matematics.AreaType;
using Matematics.Other;
using System;

namespace Matematics.Basic
{
    public class EquationSecondGradeSolver
    {
        /// <summary>
        /// X^2 Number
        /// </summary>
        public Monomio A { get; set; }
        /// <summary>
        /// X^1 Number
        /// </summary>
        public Monomio B { get; set; }
        /// <summary>
        /// Number without any letter
        /// </summary>
        public Monomio C { get; set; }

        public EquationSecondGradeSolver()
        {

        }

        public EquationSecondGradeSolver(Monomio a, Monomio b, Monomio c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Delta of the equation
        /// </summary>
        /// <returns></returns>
        public Area Delta()
        {
            //b^2 - 4 * a * c
            Area tmp = new();
            tmp.Sign = false;
            tmp.MType = Other.Type.Polynomial;
            Polynomial pol = new();
            pol.Monomio.Add(new Monomio(Math.Pow(B.Number, 2)));
            pol.Monomio.Add(new Monomio(4 * A.Number * C.Number));
            tmp.Space = pol;
            return tmp;
        }

        /// <summary>
        /// the X1 AND X2
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        public void Resolve(ref Area X1, ref Area X2)
        {
            if (Delta().Execute() >= 0)
            {
                Fraction tmp = new();

                ListOfArea listofarea = new();
                Area mon1 = new(false, new Polynomial(new Monomio(-B.Number)), Other.Type.Polynomial);
                Area mon2 = new(false, new Sqrt(2, new Area(false, Delta(), Other.Type.Sqrt)), Other.Type.Sqrt);
                listofarea.Area.Add(mon1);
                listofarea.Area.Add(mon2);

                tmp.Numerator = new Area(false, listofarea, Other.Type.ListOfArea);

                Polynomial de = new();
                de.Monomio.Add(new Monomio(2 * A.Number));
                tmp.Denominator = new Area(false, de, Other.Type.Polynomial);

                X1 = new Area(false, listofarea, Other.Type.ListOfArea);
                X2 = null;
            }
            else
            {
                throw new Exception.CannotResolveException();
            }
        }

        /// <summary>
        /// Convert X1 and X2 to a number
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        public void TakeNumber(ref double X1, ref double X2)
        {
            Area tmp1 = new();
            Area tmp2 = new();
            Resolve(ref tmp1, ref tmp2);

            try
            {
                X1 = tmp1.Execute();
                X2 = tmp2.Execute();
            }
            catch (Exception.CannotResolveException)
            {
                throw new Exception.CannotResolveException();
            }
        }
    }
}
