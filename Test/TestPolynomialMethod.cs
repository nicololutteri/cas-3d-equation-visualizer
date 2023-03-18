using Matematics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Utilities;

namespace Test
{
    [TestClass]
    public class TestPolynomialMethod
    {
        public Polynomial x = new();

        [TestInitialize]
        public void TestPolynomial()
        {
            x.Monomio.Add(new Monomio(5, new Letter[] { new Letter("a", 2), new Letter("b", 2), new Letter("c", 5) }));
            x.Monomio.Add(new Monomio(6, new Letter[] { new Letter("a", 3), new Letter("b", 4), new Letter("c", 1) }));
        }

        [TestMethod]
        public void TestGetAllLetters()
        {
            List<Letter> check = new()
            {
                new Letter("a", 1),
                new Letter("b", 1),
                new Letter("c", 1)
            };

            Assert.IsTrue(x.GetAllLetters().AreEqual<Letter>(check));
        }

        [TestMethod]
        public void TestGetAllLetterWithTheBiggestGrade()
        {
            List<Letter> check = new()
            {
                new Letter("a", 3),
                new Letter("b", 4),
                new Letter("c", 5)
            };

            Assert.IsTrue(x.GetAllLetterWithTheBiggestGrade().AreEqual<Letter>(check));
        }

        [TestMethod]
        public void TestGrade()
        {
            Assert.IsTrue(x.Grade() == 3 + 4 + 5);
        }
    }
}
