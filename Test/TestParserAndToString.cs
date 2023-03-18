using Matematics.Basic;
using Matematics.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestParserAndToString
    {
        [TestMethod]
        public void Test()
        {
            Monomio mon = new(10, new Letter());
            Polynomial cmd = new(mon);

            Area tostring = new(false, cmd, Type.Polynomial);

            Area x = Area.Parse(tostring.ToString());

            if (x.MType != Type.Polynomial)
            {
                Assert.Fail();
            }
        }
    }
}

