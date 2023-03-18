namespace Matematics
{
    /// <summary>
    /// Single factor
    /// 2 ^ 2
    /// </summary>
    public class Factor
    {
        /// <summary>
        /// The Base of the Factor
        /// </summary>
        public int Base { get; set; }
        /// <summary>
        /// The grade of the Factor
        /// </summary>
        public int Grade { get; set; }

        public Factor()
        {

        }

        public Factor(int numberbase, int grade)
        {
            Base = numberbase;
            Grade = grade;
        }

        public override string ToString()
        {
            return Base + " ^ " + Grade;
        }
    }
}
