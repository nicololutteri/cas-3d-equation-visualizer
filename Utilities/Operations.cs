using System;

namespace Utilities
{
    public class Operations
    {
        public static decimal ExcecuteSqrt(int number, int grade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Excecute the moltiplication beetween two sign
        /// True = -
        /// False = +
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Result</returns>
        public static bool MultiplicationSign(bool first, bool second)
        {
            // - -
            if (first == true && second == true)
            {
                return false;
            }
            // - +
            else if (first == true && second == false)
            {
                return true;
            }
            // + -
            else if (first == false && second == true)
            {
                return true;
            }
            // + +
            else if (first == false && second == false)
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
