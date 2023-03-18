using Matematics;
using Matematics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestDerivate
    {
        [TestMethod]
        public void TestDerivateOne()
        {
            //x^1 = 1
            Monomio todo = new(1, new Letter('x', 1));
            Monomio result;
            Monomio finale = new(1);

            Derivates d = new('x');
            result = d.ToDerivate(todo);

            Assert.IsTrue(finale.CompareMonomioLetters(result) && finale.Number == result.Number);
        }

        [TestMethod]
        public void TestDerivatesTwo()
        {
            //y * x = 1 * y
            Monomio todo = new(1, new Letter('x', 1), new Letter('y', 1));
            Monomio result;
            Monomio finale = new(1, new Letter('y', 1));

            Derivates d = new('x');
            result = d.ToDerivate(todo);

            Assert.IsTrue(finale.CompareMonomioLetters(result) && finale.Number == result.Number);
        }

        [TestMethod]
        public void TestDerivatesThree()
        {
            //3 * x ^3 * y ^ 2 = 9 * x ^ 2 * y ^ 2
            Monomio todo = new(3, new Letter('x', 3), new Letter('y', 2));
            Monomio result;
            Monomio final = new(9, new Letter('x', 2), new Letter('y', 2));

            Derivates tmp = new('x');
            result = tmp.ToDerivate(todo);

            Assert.IsTrue(final.CompareMonomioLetters(result) && final.Number == result.Number);
        }
    }
}
