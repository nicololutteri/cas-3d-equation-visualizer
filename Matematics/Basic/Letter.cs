using System;

namespace Matematics.Basic
{
    /// <summary>
    /// Letter rappresents a letter with char and grade
    /// </summary>
    [Serializable]
    public class Letter : IEquatable<Letter>, IComparable<Letter>
    {
        /// <summary>
        /// Char (example: x, y, z)
        /// </summary>
        public char Char { get; set; }
        /// <summary>
        /// Grade (example: -5, 1, 4)
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Default: Char: x, Grade: 1
        /// </summary>
        public Letter()
        {
            Char = 'x';
            Grade = 1;
        }

        public Letter(char letter)
        {
            Char = letter;
            Grade = 1;
        }

        public Letter(char letter, int grade)
        {
            Char = letter;
            Grade = grade;
        }

        public Letter(string letter, int grade)
        {
            Char = Convert.ToChar(letter);
            Grade = grade;
        }

        public override string ToString()
        {
            return Char.ToString() + "^" + Grade;
        }

        public bool IsEqualsChar(Letter x)
        {
            return Char == x.Char;
        }

        public int GetLetterGrade()
        {
            return Grade;
        }

        public bool Equals(Letter other)
        {
            return IsEqualsChar(other) && Grade == other.Grade;
        }

        public int CompareTo(Letter other)
        {
            return other.Char.CompareTo(Char);
        }
    }
}
