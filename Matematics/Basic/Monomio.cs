using Matematics.Other;
using System;
using System.Collections.Generic;
using Utilities;

namespace Matematics.Basic
{
    [Serializable]
    public class Monomio : IEquatable<Monomio>, IComparable<Monomio>
    {
        /// <summary>
        /// Double, who indicates the number of monomio
        /// </summary>
        public double Number { get; set; }
        /// <summary>
        /// The letter that the Monomio has
        /// </summary>
        public List<Letter> Char { get; set; }

        public Monomio()
        {
            Char = new List<Letter>();
        }

        public Monomio(double number)
        {
            Number = number;
            Char = new List<Letter>();
        }

        public Monomio(Letter letter)
        {
            Number = 1;
            Char = new List<Letter>
            {
                letter
            };
        }

        public Monomio(double number, Letter letter)
        {
            Number = number;
            Char = new List<Letter>
            {
                letter
            };
        }

        public Monomio(double number, Letter letter1, Letter letter2)
        {
            Number = number;
            Char = new List<Letter>
            {
                letter1,
                letter2
            };
        }

        public Monomio(double number, Letter letter1, Letter letter2, Letter letter3)
        {
            Number = number;
            Char = new List<Letter>
            {
                letter1,
                letter2,
                letter3
            };
        }

        public Monomio(double number, List<Letter> chars)
        {
            Number = number;
            Char = chars;
        }

        public Monomio(double number, Letter[] chars)
        {
            Number = number;

            Char = new List<Letter>();
            foreach (Letter x in chars)
            {
                Char.Add(x);
            }
        }

        public Monomio ExcecuteSqrt(int grade)
        {
            Monomio tmp = new();
            tmp.Number = int.Parse(Math.Sqrt(tmp.Number).ToString());
            foreach (Letter x in Char)
            {
                if (x.Grade.Equals(x))
                {
                    decimal value = Operations.ExcecuteSqrt(x.Grade, grade);
                    if (value == decimal.Floor(value))
                    {
                        tmp.Char.Add(new Letter(x.Char, (int)Math.Sqrt(x.Grade)));
                    }
                }
            }

            return tmp;
        }

        public Monomio Moltiply(Monomio m)
        {
            List<Area> tmp = new()
            {
                new Area(false, this, Other.Type.Monomio),
                new Area(false, this, Other.Type.Monomio)
            };
            return (Monomio)((Multiply)MCMAndMCD.FindMCM(tmp).Space).ListOfArea[0].Space;
        }

        public int GetGeneralGrade()
        {
            Normalize();

            int sum = 0;

            foreach (Letter x in Char)
            {
                sum += x.GetLetterGrade();
            }

            return sum;
        }

        /// <summary>
        /// Sum the letter
        /// </summary>
        public void Normalize()
        {
            for (int i = 0; i < Char.Count; i++)
            {
                if (Char[i].Grade == 0)
                {
                    Char.RemoveAt(i);
                    i = 0;
                }
            }

            List<Letter> tmp = new();

            for (int i = 0; i < Char.Count; i++)
            {
                for (int j = 0; j < Char.Count; j++)
                {
                    if (i != j)
                    {
                        if (Char[i].IsEqualsChar(Char[j]))
                        {
                            if (Char[i].Grade != 0)
                            {
                                tmp.Add(new Letter(Char[i].Char, Char[i].Grade + Char[j].Grade));
                            }
                        }
                        else
                        {
                            if (Char[i].Grade != 0)
                            {
                                tmp.Add(Char[i]);
                            }
                        }
                    }
                }
            }

            tmp.Sort((Letter x, Letter y) => x.Char.CompareTo(y.Char));
        }

        public bool CompareMonomioLetters(Monomio x)
        {
            if (x.Char.Count != Char.Count)
            {
                return false;
            }

            Normalize();
            x.Normalize();

            for (int i = 0; i < x.Char.Count; i++)
            {
                if (!x.Char[i].Equals(Char[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public Area Replace(Letter x, Area number)
        {
            Multiply tmp = new();
            foreach (Letter j in Char)
            {
                if (j.IsEqualsChar(x))
                {
                    for (int i = 0; i < j.Grade; i++)
                    {
                        tmp.Add(number);
                    }
                }
                else
                {
                    tmp.Add(new Area(false, this, Other.Type.Monomio));
                }
            }

            Monomio mono = new(Number);
            tmp.Add(new Area(false, mono, Other.Type.Monomio));

            return new Area(false, tmp, Other.Type.Multiply);
        }

        public void Reverse()
        {
            Number = 1 / Number;
            for (int i = 0; i < Char.Count; i++)
            {
                Char[i] = new Letter(Char[i].Char, -Char[i].Char);
            }
        }

        public bool ContainsLetter(Char x)
        {
            foreach (Letter z in Char)
            {
                if (z.Char == x)
                {
                    return true;
                }
            }

            return false;
        }

        public static Monomio Parse(string x)
        {
            Monomio tmp = new();
            string[] spl = x.Split(new string[] { "*", " * " }, StringSplitOptions.None);
            tmp.Number = double.Parse(spl[0]);
            for (int i = 1; i < spl.Length; i++)
            {
                string[] spl2 = spl[i].Split(new string[] { "^", " ^ " }, StringSplitOptions.None);
                tmp.Char.Add(new Letter(spl2[0], int.Parse(spl2[1])));
            }

            return tmp;
        }



        public override string ToString()
        {
            return Number.ToString() + " " + Char.ListToString(" * ");
        }

        public bool Equals(Monomio other)
        {
            return Number == other.Number && Char.AreEqual<Letter>(other.Char);
        }

        public int CompareTo(Monomio other)
        {
            throw new NotImplementedException();
        }
    }
}
