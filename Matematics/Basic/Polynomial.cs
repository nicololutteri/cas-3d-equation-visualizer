using Matematics.Other;
using System;
using System.Collections.Generic;
using Utilities;

namespace Matematics.Basic
{
    [Serializable]
    public class Polynomial
    {
        /// <summary>
        /// List of Monomio
        /// </summary>
        public List<Monomio> Monomio { get; set; }

        public Polynomial()
        {
            Monomio = new List<Monomio>();
        }

        public Polynomial(Monomio x)
        {
            Monomio = new List<Monomio>
            {
                x
            };
        }

        public Polynomial(Monomio x, Monomio y)
        {
            Monomio = new List<Monomio>
            {
                x,
                y
            };
        }

        public Polynomial(Monomio x, Monomio y, Monomio z)
        {
            Monomio = new List<Monomio>
            {
                x,
                y,
                z
            };
        }

        public override string ToString()
        {
            return Monomio.ListToString<Monomio>(" + ");
        }

        public List<Letter> GetAllLetterWithTheBiggestGrade()
        {
            List<Letter> tmp = GetAllLetters();

            foreach (Monomio x in Monomio)
            {
                foreach (Letter j in x.Char)
                {
                    //Find the bigger
                    foreach (Letter k in tmp)
                    {
                        if (k.IsEqualsChar(j))
                        {
                            if (k.Grade < j.Grade)
                            {
                                tmp[tmp.IndexOf(k)] = j;
                                break;
                            }
                        }
                    }
                }
            }

            return tmp;
        }

        public int Grade()
        {
            List<Letter> tmp = GetAllLetterWithTheBiggestGrade();

            int grade = 0;
            foreach (Letter x in tmp)
            {
                grade += x.Grade;
            }

            return grade;
        }

        public List<Letter> GetAllLetters()
        {
            List<Letter> tmp = new();

            foreach (Monomio x in Monomio)
            {
                foreach (Letter j in x.Char)
                {
                    if (tmp.FindIndex((a) => a.Char == j.Char) == -1)
                    {
                        tmp.Add(new Letter(j.Char, 1));
                    }
                }
            }

            return tmp;
        }

        public double Execute()
        {
            if (GetAllLetters().Count == 0)
            {
                double value = 0;
                foreach (Monomio x in Monomio)
                {
                    value = value + x.Number;
                }

                return value;
            }
            else
            {
                throw new Exception.CannotGetDecimalLetterArePresentException();
            }
        }

        public Area Simplify()
        {
            bool exit = false;
            for (int i = 0; i < Monomio.Count && exit == false; i++)
            {
                for (int j = 0; j < Monomio.Count; j++)
                {
                    if (i != j)
                    {
                        if (Monomio[i].CompareMonomioLetters(Monomio[j]))
                        {
                            Monomio tmp1 = Monomio[i];
                            Monomio tmp2 = Monomio[j];

                            Monomio final = new(tmp1.Number + tmp2.Number, Monomio[i].Char);

                            Monomio.RemoveAt(i);
                            Monomio.RemoveAt(j - 1); //Because of the previous delete

                            Monomio.Add(final);

                            exit = true;
                            break;
                        }
                    }
                }
            }

            if (exit == false)
            {
                return new Area(false, this, Other.Type.Polynomial);
            }
            else
            {
                return Simplify();
            }
        }

        public void Multiply(Monomio y)
        {
            for (int i = 0; i < Monomio.Count; i++)
            {
                Monomio[i] = new Monomio(Monomio[i].Number * y.Number, Monomio[i].Char);
            }
        }

        public Polynomial Moltiply(Polynomial x)
        {
            Polynomial final = new();
            foreach (Monomio t in Monomio)
            {
                foreach (Monomio z in x.Monomio)
                {
                    final.Monomio.Add(t.Moltiply(z));
                }
            }

            return final;
        }

        public void Move(Polynomial origin, Polynomial destination, Monomio x)
        {
            int pos = origin.Monomio.IndexOf(x);

            if (pos != -1)
            {
                Monomio tmp = origin.Monomio[pos];
                tmp = new Monomio(-tmp.Number, tmp.Char);

                destination.Monomio.Add(tmp);
                origin.Monomio.RemoveAt(pos);
                return;
            }
            else
            {
                throw new Exception.CannotMoveMonomioIsNotFoundException();
            }
        }

        public ListOfArea Replace(Letter x, Area a)
        {
            ListOfArea list = new();

            foreach (Monomio j in Monomio)
            {
                if (j.Char.Count != 0)
                {
                    list.Area.Add(j.Replace(x, a));
                }
                else
                {
                    list.Area.Add(new Area(false, j, Other.Type.Monomio));
                }
            }

            return list;
        }
    }
}
