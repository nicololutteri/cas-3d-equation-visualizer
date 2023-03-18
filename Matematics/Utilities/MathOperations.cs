using Matematics.Basic;
using Matematics.Other;
using System;
using System.Collections.Generic;

namespace Matematics
{
    public class MCMAndMCD
    {
        public static decimal ExcecuteSqrt(int number, int grade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find the factor of the number
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>List of the Factor</returns>
        public static List<Factor> FindNumberPrimes(double number)
        {
            List<Factor> tmp = new();

            int numbertmp = 2;
            int grade = 0;
            while (number > 1)
            {
                if (number % Math.Pow(numbertmp, grade) == 0)
                {
                    grade++;
                }
                else
                {
                    if (grade - 1 != 0)
                    {
                        tmp.Add(new Factor(numbertmp, grade - 1));
                    }
                    number = number / Math.Pow(numbertmp, grade - 1);

                    numbertmp++;
                    grade = 1;
                }
            }

            return tmp;
        }
        /// <summary>
        /// Find the factor of all the number of the list
        /// </summary>
        /// <param name="list">List of number</param>
        /// <param name="mcm">
        /// True if find mcm
        /// False if find mcd
        /// </param>
        /// <returns></returns>
        public static List<Factor> GenerateForAll(List<double> list, bool mcm)
        {
            List<Factor> tmp = new();

            foreach (double x in list)
            {
                foreach (Factor j in FindNumberPrimes(x))
                {
                    tmp.Add(j);
                }
            }

            return tmp;
        }
        /// <summary>
        /// Find the max base
        /// </summary>
        /// <param name="list">List of the search</param>
        /// <returns></returns>
        private static int FindMaxBase(List<Factor> list)
        {
            int numberbase = 0;

            foreach (Factor x in list)
            {
                if (x.Base > numberbase)
                {
                    numberbase = x.Base;
                }
            }

            return numberbase;
        }
        /// <summary>
        /// Search the max base number in the number list
        /// </summary>
        /// <param name="number">List of the search</param>
        /// <param name="basenumber">Basenumber to find</param>
        /// <param name="findmax">
        /// True find the max
        /// False find the min
        /// </param>
        /// <returns></returns>
        private static Factor FindMax(List<Factor> number, int basenumber, bool findmax)
        {
            Factor tmp = null;

            foreach (Factor x in number)
            {
                if ((tmp == null || (x.Grade > tmp.Grade && findmax == true) || (x.Grade < tmp.Grade && findmax == false)) && x.Base == basenumber)
                {
                    tmp = x;
                }
            }

            if (tmp == null)
            {
                return new Factor(basenumber, 0);
            }

            return tmp;
        }
        public static double FindNumberFinal(List<double> list, bool mcm)
        {
            List<Factor> tmp = GenerateForAll(list, mcm);
            int maxbase = FindMaxBase(tmp);

            double number = 1;
            for (int basetofin = 2; basetofin <= maxbase; basetofin++)
            {
                Factor f = FindMax(tmp, basetofin, true);
                number *= Math.Pow(f.Base, f.Grade);
            }

            return number;
        }

        /// <summary>
        /// Find the maximun grade of the letter
        /// </summary>
        /// <param name="leter"></param>
        /// <param name="mcm"></param>
        /// <returns></returns>
        public static List<Letter> FindLetterPrimes(Letter leter, bool mcm)
        {
            //ab^2
            List<Letter> tmp = new();
            foreach (Letter x in tmp)
            {
                Letter now = null;

                foreach (Letter j in tmp)
                {
                    if (x.IsEqualsChar(j))
                    {
                        if (now == null || (x.Grade > now.Grade && mcm == true) || (x.Grade < now.Grade && mcm == false))
                        {
                            now = x;
                        }
                    }
                }

                tmp.Add(now);
            }

            return tmp;
        }
        public static List<Letter> GenerateForAllLetter(List<List<Letter>> list, bool mcm)
        {
            List<Letter> tmp = Convert(list);
            List<Letter> alltheletter = FindAllLetter(tmp);
            List<Letter> final = new();

            foreach (Letter x in alltheletter)
            {
                final.Add(FindMaxLetter(alltheletter, x, mcm));
            }

            return final;
        }
        private static Letter FindMaxLetter(List<Letter> list, Letter l, bool mcm)
        {
            Letter grademax = new(l.Char, 0);

            foreach (Letter x in list)
            {
                if (x.Char == l.Char && ((mcm == true && grademax.Grade < x.Grade) || (mcm == false && grademax.Grade > x.Grade)))
                {
                    grademax = x;
                }
            }

            return grademax;
        }
        private static List<Letter> FindAllLetter(List<Letter> list)
        {
            List<Letter> tmp = new();

            foreach (Letter x in list)
            {
                if (tmp.FindIndex((f) => x.Char == f.Char) == -1)
                {
                    tmp.Add(x);
                }
                else
                {

                }
            }

            return tmp;
        }
        private static List<Letter> Convert(List<List<Letter>> list)
        {
            List<Letter> tmp = new();

            foreach (List<Letter> x in list)
            {
                foreach (Letter j in x)
                {
                    tmp.Add(j);
                }
            }

            return tmp;
        }

        public static Area FindMCM(List<Area> areas)
        {
            Area tmp = new(false, new Multiply(), Other.Type.Multiply);
            List<double> numberstofin = new();
            List<List<Letter>> lettertofin = new();

            foreach (Area x in areas)
            {
                switch (x.MType)
                {
                    case Other.Type.Monomio:
                        numberstofin.Add(((Monomio)x.Space).Number);
                        lettertofin.Add(((Monomio)x.Space).Char);
                        break;
                    case Other.Type.Polynomial:
                        ((Multiply)tmp.Space).Add(x);
                        break;
                    case Other.Type.Multiply:
                        ((Multiply)tmp.Space).Add(FindMCM(((Multiply)x.Space).ListOfArea));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            if (numberstofin.Count != 0 && lettertofin.Count != 0)
            {
                ((Multiply)tmp.Space).Add(new Area(false, new Monomio(FindNumberFinal(numberstofin, true), GenerateForAllLetter(lettertofin, true)), Other.Type.Monomio));
            }

            return tmp;
        }

        private Factor Find(bool max, List<Factor> tmp, int Number)
        {
            if (tmp.Count == 0)
            {
                return null;
            }

            Factor s = null;
            foreach (Factor x in tmp)
            {
                if (s == null || s.Base == Number && ((x.Grade > s.Grade && max == true) || (x.Grade < s.Grade && max == false)))
                {
                    s = x;
                }
                else
                {

                }
            }

            return s;
        }

        public static int FindMCD(List<int> numbers)
        {
            //List<List<Factor>> tmp = new List<List<Factor>>();
            //foreach (int x in numbers)
            //{
            //    tmp.Add(FindNumberPrimes(x));
            //}

            //int number = 0;
            //foreach (List<Factor> x in tmp)
            //{
            //    foreach (Factor y in x)
            //    {

            //    }
            //}

            throw new NotImplementedException();
        }
    }
}
