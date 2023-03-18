using Matematics.AreaType;
using Matematics.Basic;
using System;
using Utilities;

namespace Matematics.Other
{
    public enum Type
    {
        Polynomial,
        Sqrt,
        ListOfArea,
        Equation,
        Cos,
        Sen,
        Tan,
        Monomio,
        Fraction,
        Multiply,
        Pow
    }

    [Serializable]
    public class Area
    {
        /// <summary>
        /// Sign of the area (True -, False +)
        /// </summary>
        public bool Sign { get; set; }
        /// <summary>
        /// Space that can contains Sqrt....
        /// </summary>
        public object Space { get; set; }
        /// <summary>
        /// Type of the area
        /// </summary>
        public Type MType { get; set; }

        public Area()
        {

        }

        public Area(bool sign, object space, Type type)
        {
            Sign = sign;
            Space = space;
            MType = type;
        }

        /// <summary>
        /// Replace a letter with a variable
        /// (It modify this object)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="number"></param>
        public void Replace(Letter x, Area a)
        {
            switch (MType)
            {
                case Type.Polynomial:
                    Space = ((Polynomial)Space).Replace(x, a);
                    MType = Type.ListOfArea;
                    break;
                case Type.Sqrt:
                    ((Sqrt)Space).Area.Replace(x, a);
                    break;
                case Type.ListOfArea:
                    ((ListOfArea)Space).Replace(x, a);
                    break;
                case Type.Equation:
                    ((Equation)Space).Replace(x, a);
                    break;
                case Type.Cos:
                    ((Cos)Space).Argument.Replace(x, a);
                    break;
                case Type.Sen:
                    ((Sen)Space).Argument.Replace(x, a);
                    break;
                case Type.Tan:
                    ((Tan)Space).Argument.Replace(x, a);
                    break;
                case Type.Monomio:
                    Area xc = ((Monomio)Space).Replace(x, a);
                    Space = xc.Space;
                    MType = xc.MType;

                    break;
                case Type.Fraction:
                    ((Fraction)Space).Replace(x, a);
                    break;
                case Type.Multiply:
                    ((Multiply)Space).Replace(x, a);
                    break;
                case Type.Pow:
                    ((Pow)Space).Area.Replace(x, a);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Simplify the area
        /// </summary>
        /// <param name="superior"></param>
        public void Simplify(Area superior)
        {
            switch (MType)
            {
                case Type.Polynomial:
                    ((Polynomial)Space).Simplify();
                    break;
                case Type.Sqrt:
                    ((Sqrt)Space).Area.Simplify(superior);
                    break;
                case Type.Equation:
                    ((Equation)Space).Left.Simplify(superior);
                    ((Equation)Space).Right.Simplify(superior);
                    break;
                case Type.Cos:
                    ((Cos)Space).Argument.Simplify(superior);
                    break;
                case Type.Sen:
                    ((Sen)Space).Argument.Simplify(superior);
                    break;
                case Type.Tan:
                    ((Tan)Space).Argument.Simplify(superior);
                    break;
                case Type.Monomio:
                    ((Monomio)Space).Normalize();
                    break;
                case Type.Fraction:
                    ((Fraction)Space).Simplify();
                    break;
                case Type.Multiply:
                    Multiply objecttmp = (Multiply)Space;

                    Area actual = objecttmp.ListOfArea[0];
                    for (int i = 1; i < objecttmp.ListOfArea.Count; i++)
                    {
                        Multiply(actual, objecttmp.ListOfArea[i]);
                    }

                    superior = actual;
                    break;
                case Type.Pow:
                    ((Pow)Space).Area.Simplify(superior);
                    break;
                case Type.ListOfArea:
                    ListOfArea objecttmp2 = (ListOfArea)Space;

                    Area actual2 = objecttmp2.Area[0];
                    for (int i = 1; i < objecttmp2.Area.Count; i++)
                    {
                        Sum(actual2, objecttmp2.Area[i]);
                    }

                    superior = actual2;
                    break;
                default:
                    throw new Exception.CannotResolveException();
            }
        }

        /// <summary>
        /// Gives the number of the area
        /// </summary>
        /// <returns></returns>
        public double Execute()
        {
            switch (MType)
            {
                case Type.ListOfArea:
                    double value = 0;
                    foreach (Area x in ((ListOfArea)Space).Area)
                    {
                        if (x.Sign)
                        {
                            value = value - x.Execute();
                        }
                        else
                        {
                            value = value + x.Execute();
                        }
                    }
                    return value;
                case Type.Polynomial:
                    return ((Polynomial)Space).Execute();
                case Type.Sqrt:
                    return ((Sqrt)Space).ExecuteValue();
                case Type.Equation:
                    return (((Equation)Space).SolveTakeNumber(new Letter('y')));
                case Type.Cos:
                    return ((Cos)Space).TakeNumber();
                case Type.Sen:
                    return ((Sen)Space).TakeNumber();
                case Type.Tan:
                    return ((Tan)Space).TakeNumber();
                case Type.Monomio:
                    if (((Monomio)Space).Char.Count == 0)
                    {
                        return ((Monomio)Space).Number;
                    }
                    else
                    {
                        throw new Exception.CannotGetDecimalLetterArePresentException();
                    }
                case Type.Fraction:
                    return ((Fraction)Space).TakeANumber();
                case Type.Multiply:
                    return ((Multiply)Space).TakeNumber();
                case Type.Pow:
                    return ((Pow)Space).TakeNumber();
                default:
                    throw new Exception.CannotResolveException();
            }
        }

        /// <summary>
        /// Sum the first and the second
        /// </summary>
        /// <param name="first">First area</param>
        /// <param name="second">Second area</param>
        /// <returns>Result</returns>
        public static Area Sum(Area first, Area second)
        {
            if (first.MType == Type.Equation || second.MType == Type.Equation)
            {
                throw new Exception.CannotResolveException();
            }

            switch (first.MType)
            {
                case Type.Polynomial:
                    foreach (Monomio x in ((Polynomial)first.Space).Monomio)
                    {
                        ((Polynomial)second.Space).Monomio.Add(x);
                    }
                    first.Simplify(null);
                    return new Area(false, first, Type.Polynomial);
                case Type.Sqrt:
                    throw new NotImplementedException();
                case Type.ListOfArea:
                    ListOfArea objecttmp = ((ListOfArea)first.Space);
                    Area actual = objecttmp.Area[0];
                    for (int i = 1; i < objecttmp.Area.Count; i++)
                    {
                        actual = Sum(actual, objecttmp.Area[i]);
                    }
                    return actual;
                case Type.Cos:
                    throw new NotImplementedException();
                case Type.Sen:
                    throw new NotImplementedException();
                case Type.Tan:
                    throw new NotImplementedException();
                case Type.Monomio:
                    switch (second.MType)
                    {
                        case Type.Monomio:
                            Polynomial tmpl = new();
                            tmpl.Monomio.Add((Monomio)first.Space);
                            tmpl.Monomio.Add((Monomio)second.Space);
                            tmpl.Simplify();
                            return new Area(false, tmpl, Type.Polynomial);
                        case Type.Polynomial:
                            ((Polynomial)second.Space).Monomio.Add((Monomio)first.Space);
                            ((Polynomial)second.Space).Simplify();
                            return new Area(false, second, Type.Polynomial);
                        default:
                            throw new NotImplementedException();
                    }
                case Type.Fraction:
                    switch (second.MType)
                    {
                        case Type.Fraction:
                            return ((Fraction)first.Space).Sum((Fraction)second.Space);
                        case Type.Polynomial:
                            Fraction f = new();
                            f.Numerator = second;
                            f.Denominator = new Area(false, new Polynomial(new Monomio(1)), Type.Polynomial);
                            return ((Fraction)first.Space).Sum(f);
                        default:
                            throw new NotImplementedException();
                    }

                case Type.Multiply:
                    switch (second.MType)
                    {
                        case Type.Polynomial:
                            ListOfArea tmptmptmp = new();
                            tmptmptmp.Area.Add(first);
                            tmptmptmp.Area.Add(second);

                            return new Area(false, tmptmptmp, Type.ListOfArea);
                        case Type.Sqrt:
                            throw new NotImplementedException();
                        case Type.ListOfArea:
                            ListOfArea tmptmp = new();
                            tmptmp.Area.Add(first);
                            tmptmp.Area.Add(second);

                            return new Area(false, tmptmp, Type.ListOfArea);
                        case Type.Cos:
                            throw new NotImplementedException();
                        case Type.Sen:
                            throw new NotImplementedException();
                        case Type.Tan:
                            throw new NotImplementedException();
                        case Type.Monomio:
                            ListOfArea tmp = new();
                            tmp.Area.Add(first);
                            tmp.Area.Add(second);

                            return new Area(false, tmp, Type.ListOfArea);
                        case Type.Fraction:
                            ListOfArea tmpl = new();
                            tmpl.Area.Add(first);
                            tmpl.Area.Add(second);

                            return new Area(false, tmpl, Type.ListOfArea);
                        case Type.Multiply:
                            ListOfArea tmpm = new();
                            tmpm.Area.Add(first);
                            tmpm.Area.Add(second);

                            return new Area(false, tmpm, Type.ListOfArea);
                        case Type.Pow:
                            ListOfArea tmpp = new();
                            tmpp.Area.Add(first);
                            tmpp.Area.Add(second);

                            return new Area(false, tmpp, Type.ListOfArea);
                        default:
                            throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Find the letter
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>
        /// True: if contains the letter
        /// False: if not contains the letter
        /// </returns>
        public bool ContainsLetter(Letter letter)
        {
            switch (MType)
            {
                case Type.Cos:
                    return ((Cos)Space).Argument.ContainsLetter(letter);
                case Type.Fraction:
                    return ((Fraction)Space).Denominator.ContainsLetter(letter) || ((Fraction)Space).Numerator.ContainsLetter(letter);
                case Type.ListOfArea:
                    foreach (Area x in ((ListOfArea)Space).Area)
                    {
                        if (x.ContainsLetter(letter) == true)
                        {
                            return true;
                        }
                    }

                    return false;
                case Type.Monomio:
                    return ((Monomio)Space).ContainsLetter('x');
                case Type.Multiply:
                    foreach (Area x in ((Multiply)Space).ListOfArea)
                    {
                        if (x.ContainsLetter(letter) == true)
                        {
                            return true;
                        }
                    }

                    return false;
                case Type.Polynomial:
                    foreach (Letter x in ((Polynomial)Space).GetAllLetters())
                    {
                        if (x.Char == letter.Char)
                        {
                            return true;
                        }
                    }

                    return false;
                case Type.Pow:
                    return ((Pow)Space).Area.ContainsLetter(letter);
                case Type.Sen:
                    return ((Sen)Space).Argument.ContainsLetter(letter);
                case Type.Sqrt:
                    return ((Sqrt)Space).Area.ContainsLetter(letter);
                case Type.Tan:
                    return ((Tan)Space).Argument.ContainsLetter(letter);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Muliply the first and the second area
        /// </summary>
        /// <param name="first">First area</param>
        /// <param name="second">Second area</param>
        /// <returns>Result</returns>
        public static Area Multiply(Area first, Area second)
        {
            if (first.MType == Type.Equation || second.MType == Type.Equation)
            {
                throw new Exception.CannotResolveException();
            }

            switch (first.MType)
            {
                case Type.Polynomial:
                    switch (second.MType)
                    {
                        case Type.Polynomial:
                            return new Area(false, ((Polynomial)first.Space).Moltiply((Polynomial)second.Space), Type.Polynomial);
                        case Type.Sqrt:

                        case Type.ListOfArea:
                            return Multiply(second, first);
                        case Type.Cos:

                        case Type.Sen:

                        case Type.Tan:

                        case Type.Monomio:

                        case Type.Fraction:

                        case Type.Multiply:

                        case Type.Pow:

                        default:
                            throw new NotImplementedException();
                    }
                case Type.Sqrt:
                    throw new NotImplementedException();
                case Type.ListOfArea:
                    switch (second.MType)
                    {
                        case Type.Polynomial:

                        case Type.Sqrt:
                            throw new NotImplementedException();
                        case Type.ListOfArea:
                            ListOfArea final = new();

                            foreach (Area x in ((ListOfArea)first.Space).Area)
                            {
                                foreach (Area j in ((ListOfArea)second.Space).Area)
                                {
                                    final.Area.Add(Multiply(x, j));
                                }
                            }

                            return new Area(false, final, Type.ListOfArea);
                        case Type.Cos:
                            throw new NotImplementedException();
                        case Type.Sen:
                            throw new NotImplementedException();
                        case Type.Tan:
                            throw new NotImplementedException();
                        case Type.Monomio:
                            ListOfArea tmp2 = new();

                            foreach (Area x in ((ListOfArea)first.Space).Area)
                            {
                                Multiply tmp4 = new();
                                tmp4.Add(first);
                                tmp4.Add(second);

                                tmp2.Area.Add(new Area(false, tmp4, Type.Multiply));
                            }

                            return new Area(false, tmp2, Type.ListOfArea);
                        case Type.Fraction:

                        case Type.Multiply:

                        case Type.Pow:

                        default:
                            throw new NotImplementedException();
                    }
                case Type.Cos:
                    throw new NotImplementedException();
                case Type.Sen:
                    throw new NotImplementedException();
                case Type.Tan:
                    throw new NotImplementedException();
                case Type.Monomio:
                    switch (second.MType)
                    {
                        case Type.Polynomial:
                            ((Polynomial)second.Space).Multiply((Monomio)first.Space);
                            return second;
                        case Type.Sqrt:

                        case Type.ListOfArea:
                            return Multiply(second, first);
                        case Type.Cos:
                            throw new NotImplementedException();
                        case Type.Sen:
                            throw new NotImplementedException();
                        case Type.Tan:
                            throw new NotImplementedException();
                        case Type.Monomio:
                            Multiply newm = new();
                            newm.Add(first);
                            newm.Add(second);

                            return new Area(false, newm, Type.Multiply);
                        case Type.Fraction:

                        case Type.Multiply:

                        case Type.Pow:

                        default:
                            throw new NotImplementedException();
                    }
                case Type.Fraction:
                    switch (second.MType)
                    {
                        case Type.Polynomial:

                        case Type.Sqrt:
                            throw new NotImplementedException();
                        case Type.ListOfArea:
                            return Multiply(second, first);
                        case Type.Cos:
                            throw new NotImplementedException();
                        case Type.Sen:
                            throw new NotImplementedException();
                        case Type.Tan:
                            throw new NotImplementedException();
                        case Type.Monomio:
                            throw new NotImplementedException();
                        case Type.Fraction:

                        case Type.Multiply:

                        case Type.Pow:

                        default:
                            throw new NotImplementedException();

                    }
                case Type.Multiply:
                    switch (second.MType)
                    {
                        case Type.Polynomial:

                        case Type.Sqrt:
                            throw new NotImplementedException();
                        case Type.ListOfArea:
                            return Multiply(second, first);
                        case Type.Cos:
                            throw new NotImplementedException();
                        case Type.Sen:
                            throw new NotImplementedException();
                        case Type.Tan:
                            throw new NotImplementedException();
                        case Type.Monomio:
                            throw new NotImplementedException();
                        case Type.Fraction:

                        case Type.Multiply:
                            Multiply tmpy = new();
                            tmpy.Add(first);
                            tmpy.Add(second);

                            return new Area(false, tmpy, Type.Multiply);
                        case Type.Pow:

                        default:
                            throw new NotImplementedException();

                    }
                case Type.Pow:

                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string signtmp = "";
            if (Sign == true)
            {
                signtmp = "-";
            }
            else
            {
                signtmp = "+";
            }

            string tmp = "";

            switch (MType)
            {
                case Type.Equation:
                    string sign = "";

                    switch (((Equation)Space).Sign)
                    {
                        case Basic.Sign.Equals:
                            sign = "=";
                            break;
                        case Basic.Sign.Maximun:
                            sign = ">";
                            break;
                        case Basic.Sign.MaximunEquals:
                            sign = ">=";
                            break;
                        case Basic.Sign.Minor:
                            sign = "<";
                            break;
                        case Basic.Sign.MinorEquals:
                            sign = "<=";
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    return signtmp + "E[" + ((Equation)Space).Left.ToString() + sign + ((Equation)Space).Right.ToString() + "]";
                case Type.ListOfArea:
                    foreach (Area x in ((ListOfArea)Space).Area)
                    {
                        tmp += ";" + x.ToString();
                    }

                    if (tmp.Length != 0)
                    {
                        tmp = tmp.Remove(0, 1);
                    }

                    return signtmp + "L[" + tmp + "]";
                case Type.Polynomial:
                    foreach (Monomio x in ((Polynomial)Space).Monomio)
                    {
                        tmp += ";" + x.ToString();
                    }

                    if (tmp.Length != 0)
                    {
                        tmp = tmp.Remove(0, 1);
                    }

                    return signtmp + "P[" + tmp + "]";
                case Type.Sen:
                    return signtmp + "S[" + ((Sen)Space).ToString() + "]";
                case Type.Cos:
                    return signtmp + "C[" + ((Cos)Space).ToString() + "]";
                case Type.Tan:
                    return signtmp + "T[" + ((Tan)Space).ToString() + "]";
                case Type.Monomio:
                    return signtmp + "M[" + ((Monomio)Space).ToString() + "]";
                case Type.Fraction:
                    return signtmp + "F[" + ((Fraction)Space).Numerator.ToString() + ";" + ((Fraction)Space).Denominator.ToString() + "]";
                case Type.Multiply:
                    foreach (Area x in ((Multiply)Space).ListOfArea)
                    {
                        tmp += ";" + x.ToString();
                    }

                    if (tmp.Length != 0)
                    {
                        tmp = tmp.Remove(0, 1);
                    }

                    return signtmp + "M[" + tmp + "]";
                case Type.Pow:
                    return signtmp + "PW[" + ((Pow)Space).Area.ToString() + ";" + ((Pow)Space).Exponential.ToString() + "]";
                default:
                    throw new NotImplementedException();
            }
        }

        public static Area Parse(string forparse)
        {
            // E[P[y]=P[5x]]

            bool signtmp = false;
            if (forparse.StartsWith("+"))
            {
                signtmp = false;
            }
            else
            {
                signtmp = true;
            }
            forparse = forparse.Remove(0, 1);

            if (forparse.StartsWith("E"))
            {
                Equation t = new();
                string[] tmp = forparse.CutStringForParse().Split(new string[] { "=", ">=", ">", "<=", "<" }, StringSplitOptions.None);
                Area left = Parse(tmp[0]);
                Area right = Parse(tmp[1]);

                t.Left = left;
                t.Right = right;

                forparse = forparse.CutStringForParse().Remove(0, tmp[0].Length);

                if (forparse.StartsWith("="))
                {
                    t.Sign = Basic.Sign.Equals;
                }
                else if (forparse.StartsWith("<="))
                {
                    t.Sign = Basic.Sign.MinorEquals;
                }
                else if (forparse.StartsWith("<"))
                {
                    t.Sign = Basic.Sign.Minor;
                }
                else if (forparse.StartsWith(">="))
                {
                    t.Sign = Basic.Sign.MaximunEquals;
                }
                else if (forparse.StartsWith(">"))
                {
                    t.Sign = Basic.Sign.Maximun;
                }

                return new Area(signtmp, t, Type.Equation);
            }
            else if (forparse.StartsWith("L"))
            {
                ListOfArea tmp = new();
                string[] spl = forparse.CutStringForParse().Split(new string[] { ";", " ; " }, StringSplitOptions.None);
                foreach (string x in spl)
                {
                    tmp.Area.Add(Parse(x));
                }
                return new Area(signtmp, tmp, Type.ListOfArea);
            }
            else if (forparse.StartsWith("P"))
            {
                Polynomial tmp = new();
                string[] spl = forparse.CutStringForParse().Split(new string[] { ";", " ; ", "+", " + ", " - " }, StringSplitOptions.None);
                foreach (string x in spl)
                {
                    tmp.Monomio.Add(Monomio.Parse(x));
                }
                return new Area(signtmp, tmp, Type.Polynomial);
            }
            else if (forparse.StartsWith("S"))
            {
                Sen tmp = new();
                tmp.Argument = Parse(forparse.CutStringForParse().Substring(forparse.Length - 2 - 1, 1));
                return new Area(signtmp, tmp, Type.Sen);
            }
            else if (forparse.StartsWith("C"))
            {
                Cos tmp = new();
                tmp.Argument = Parse(forparse.CutStringForParse());
                return new Area(signtmp, tmp, Type.Cos);
            }
            else if (forparse.StartsWith("T"))
            {
                Tan tmp = new();
                tmp.Argument = Parse(forparse.CutStringForParse());
                return new Area(signtmp, tmp, Type.Tan);
            }
            else if (forparse.StartsWith("M"))
            {
                throw new NotImplementedException();
            }
            else if (forparse.StartsWith("F"))
            {
                Fraction tmp = new();
                string[] spl = forparse.CutStringForParse().Split(new string[] { ";", " ; " }, StringSplitOptions.None);
                tmp.Numerator = Parse(spl[0]);
                tmp.Denominator = Parse(spl[1]);
                return new Area(signtmp, tmp, Type.Fraction);
            }
            else if (forparse.StartsWith("M"))
            {
                Multiply tmp = new();
                string[] spl = forparse.CutStringForParse().Split(new string[] { ";", " ; " }, StringSplitOptions.None);
                foreach (string x in spl)
                {
                    tmp.Add(Parse(x));
                }
                return new Area(signtmp, tmp, Type.Multiply);
            }
            else if (forparse.StartsWith("PW"))
            {
                Pow tmp = new();
                string[] spl = forparse.CutStringForParse().Split(new string[] { ";", " ; " }, StringSplitOptions.None);
                tmp.Area = Parse(spl[0]);
                tmp.Exponential = Convert.ToInt32(spl[1]);
                return new Area(signtmp, tmp, Type.Pow);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
