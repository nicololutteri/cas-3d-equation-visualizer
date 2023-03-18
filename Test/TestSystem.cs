using Matematics.Algebric;
using Matematics.Basic;
using Matematics.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestSystem
    {
        [TestMethod]
        public void TestSystemOne()
        {
            // y = - x + 1
            // x = y + 1

            // y = - x + 1
            // x = - x + 1 + 1

            // y = - x + 1
            // x = - x + 2

            // y = x + 1
            // x + x = 2

            // y = x + 1
            // 2x = 2

            // y = x + 1
            // x = 1

            // y = - 1 + 1
            // x = 1

            // y = 0
            // x = 1

            SystemEquation tmp = new();
            Equation first = new();
            first.Left = new Area(false, new Polynomial(new Monomio(new Letter('y'))), Type.Polynomial);
            first.Right = new Area(false, new Polynomial(new Monomio(-1, new Letter('x')), new Monomio(2)), Type.Polynomial);
            Equation second = new();
            second.Left = new Area(false, new Polynomial(new Monomio(new Letter('x'))), Type.Polynomial);
            second.Right = new Area(false, new Polynomial(new Monomio(new Letter('y')), new Monomio(1)), Type.Polynomial);

            tmp.Equations.Add(first);
            tmp.Equations.Add(second);

            tmp.Solve();

            if (tmp.Equations[0].Right.Execute() != 0)
            {
                Assert.Fail();
            }
            if (tmp.Equations[1].Right.Execute() != 1)
            {
                Assert.Fail();
            }
        }
    }
}
