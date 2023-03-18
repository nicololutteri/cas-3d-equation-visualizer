using Matematics.Other;
using System;
using System.Collections.Generic;

namespace Matematics.Basic
{
    public enum Sign
    {
        MinorEquals,
        MaximunEquals,
        Equals,
        Minor,
        Maximun
    }

    [Serializable]
    public class Equation
    {
        /// <summary>
        /// Left of the equation
        /// </summary>
        public Area Left { get; set; }
        /// <summary>
        /// Right of the equation
        /// </summary>
        public Area Right { get; set; }

        /// <summary>
        /// Sign of the element
        /// If it is a Equation [Sign = Equals]
        /// otherwise if it is a disequation [Sign = Minor, etc]
        /// </summary>
        public Sign Sign { get; set; }

        public Equation()
        {
            Left = new Area();
            Right = new Area();

            Sign = Sign.Equals;
        }

        public Equation(Area left, Area right)
        {
            Left = left;
            Right = right;

            Sign = Sign.Equals;
        }

        public Equation(Area left, Area right, Sign sign)
        {
            Left = left;
            Right = right;

            Sign = sign;
        }

        public void Move(Polynomial left, Polynomial right, bool takefromleft, Monomio mon)
        {
            if (takefromleft)
            {
                Part(left, right, mon);
            }
            else
            {
                Part(right, left, mon);
            }
        }
        private void Part(Polynomial left, Polynomial right, Monomio mon)
        {
            foreach (Monomio x in left.Monomio)
            {
                if (x.Equals(mon))
                {
                    left.Monomio.Remove(mon);
                    mon.Number = -mon.Number;
                    right.Monomio.Add(mon);
                    return;
                }
                else
                {

                }
            }
        }

        public List<Equation> Solve(Letter forwhichletter)
        {
            List<Equation> tmp = new();
            Polynomial final = new();
            final.Monomio.Add(new Monomio(1, new Letter[] { new Letter(forwhichletter.Char, 1) }));

            Left.Simplify(null);
            Right.Simplify(null);

            Equation eq = Move(false, forwhichletter);
            if (eq != null)
            {
                tmp.Add(eq);
            }

            eq = Move(true, null);
            if (eq != null)
            {
                tmp.Add(eq);
            }

            Left = eq.Left;
            Right = eq.Right;

            Moltiply(true, new Monomio(1 / FindNumber(Left, forwhichletter)));

            return tmp;
        }
        public double SolveTakeNumber(Letter forwhichletter)
        {
            Solve(forwhichletter);
            return Right.Execute();
        }

        public double FindNumber(Area x, Letter forwhichletter)
        {
            switch (x.MType)
            {
                case Other.Type.Polynomial:
                    return ((Polynomial)x.Space).Monomio[0].Number;
                case Other.Type.Sqrt:
                    throw new NotImplementedException();
                case Other.Type.ListOfArea:
                    return FindNumber(((ListOfArea)x.Space).Area[0], forwhichletter);
                case Other.Type.Cos:
                    throw new NotImplementedException();
                case Other.Type.Sen:
                    throw new NotImplementedException();
                case Other.Type.Tan:
                    throw new NotImplementedException();
                case Other.Type.Monomio:
                    return ((Monomio)x.Space).Number;
                case Other.Type.Fraction:
                    throw new NotImplementedException();
                case Other.Type.Multiply:
                    return FindNumber(((Multiply)x.Space).ListOfArea[0], forwhichletter);
                case Other.Type.Pow:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public void Moltiply(bool StartWithLeft, Monomio x)
        {
            if (StartWithLeft)
            {
                PartMolt(Left, x);

                PartMolt(Right, x);
            }
            else
            {
                PartMolt(Right, x);

                PartMolt(Left, x);
            }
        }
        private void PartMolt(Area area, Monomio x)
        {
            area = Area.Multiply(area, new Area(false, x, Other.Type.Monomio));
            area.Simplify(area);
        }

        /// <summary>
        /// Move the different type
        /// If the Type contains forwhichletter don't move
        /// </summary>
        /// <param name="MoveLeftArea">The area of move</param>
        /// <param name="forwhichletter">Letter for check</param>
        /// <returns></returns>
        private Equation Move(bool MoveLeftArea, Letter forwhichletter)
        {
            ListOfArea tmpl = new();
            Area LeftNew = new(false, tmpl, Other.Type.ListOfArea);
            ListOfArea tmpr = new();
            Area RightNew = new(false, tmpr, Other.Type.ListOfArea);

            //y + 1 = 2 + 5 
            Equation final = new();
            if (MoveLeftArea)
            {
                Left = MoveDoWork(Left, ref tmpr, forwhichletter);
                tmpr.Area.Add(Right);

                final.Left = Left;
                final.Right = RightNew;
            }
            else
            {
                Right = MoveDoWork(Right, ref tmpl, forwhichletter);
                tmpl.Area.Add(Left);

                final.Left = LeftNew;
                final.Right = Right;
            }

            return final;
        }

        private static Area MoveDoWork(Area origin, ref ListOfArea destination, Letter forwhichletter)
        {
            ListOfArea newsource = new();
            Area a = new(false, newsource, Other.Type.ListOfArea);

            switch (origin.MType)
            {
                case Other.Type.Polynomial:
                    foreach (Monomio x in ((Polynomial)origin.Space).Monomio)
                    {
                        if ((forwhichletter != null && x.ContainsLetter(forwhichletter.Char)) || (forwhichletter == null && x.Char.Count == 0))
                        {
                            Area tmp = new(true, x, Other.Type.Monomio);
                            destination.Area.Add(tmp);
                        }
                        else
                        {
                            newsource.Area.Add(new Area(false, x, Other.Type.Monomio));
                        }
                    }
                    break;
                case Other.Type.Sqrt:
                    throw new NotImplementedException();
                case Other.Type.ListOfArea:
                    foreach (Area x in ((ListOfArea)origin.Space).Area)
                    {
                        if (x.ContainsLetter(forwhichletter))
                        {
                            x.Sign = !x.Sign;
                            destination.Area.Add(x);
                        }
                        else
                        {
                            newsource.Area.Add(x);
                        }
                    }
                    break;
                case Other.Type.Cos:
                    throw new NotImplementedException();
                case Other.Type.Sen:
                    throw new NotImplementedException();
                case Other.Type.Tan:
                    throw new NotImplementedException();
                case Other.Type.Monomio:
                    if (((Monomio)origin.Space).ContainsLetter(forwhichletter.Char))
                    {
                        ((Monomio)origin.Space).Number = -((Monomio)origin.Space).Number;
                        destination.Area.Add(origin);
                    }
                    else
                    {
                        newsource.Area.Add(new Area(false, origin, Other.Type.Monomio));
                    }
                    break;
                case Other.Type.Fraction:
                    throw new NotImplementedException();
                case Other.Type.Multiply:
                    foreach (Area x in ((Multiply)origin.Space).ListOfArea)
                    {
                        if (x.ContainsLetter(forwhichletter))
                        {
                            x.Sign = !x.Sign;
                            destination.Area.Add(x);
                        }
                        else
                        {
                            newsource.Area.Add(x);
                        }
                    }
                    break;
                case Other.Type.Pow:
                    throw new NotImplementedException();
            }

            return a;
        }

        public void Replace(Letter letter, Area a)
        {
            Left.Replace(letter, a);
            Right.Replace(letter, a);
        }
    }
}