using Matematics.Basic;
using Matematics.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class TestFindMCM
    {
        [TestMethod]
        public void NumberTest()
        {
            Area a1 = new(false, new Monomio(4), Type.Monomio);
            Area a2 = new(false, new Monomio(9), Type.Monomio);
            List<Area> list = new()
            {
                a1,
                a2
            };

            if (((Multiply)Matematics.MCMAndMCD.FindMCM(list).Space).TakeNumber() != 9 * 4)
            {
                Assert.Fail();
            }
        }
    }
}
