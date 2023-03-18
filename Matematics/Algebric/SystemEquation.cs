using Matematics.Basic;
using System.Collections.Generic;

namespace Matematics.Algebric
{
    public class SystemEquation
    {
        /// <summary>
        /// List of the Equation of the System
        /// </summary>
        public List<Equation> Equations { get; set; }

        public SystemEquation()
        {
            Equations = new List<Equation>();
        }

        public SystemEquation(List<Equation> equations)
        {
            Equations = equations;
        }

        public void Solve()
        {
            List<Equation> copy = Utilities.Basic.DeepCopy<List<Equation>>(Equations);

            copy[0].Solve(new Letter('y'));

            copy[1].Replace(new Letter('y', 1), copy[0].Right);
            copy[1].Solve(new Letter('x', 1));

            copy[0].Replace(new Letter('x', 1), copy[1].Right);
            copy[0].Solve(new Letter('y', 1));
        }
    }
}
