using Matematics.Basic;
using Matematics.Graphic;
using Matematics.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestGraphic
    {
        [TestMethod]
        public void Bidimensional()
        {
            Area area = new();
            Equation equation = new();

            Polynomial Left = new();
            Left.Monomio.Add(new Monomio(1, new Letter("y", 1)));
            Polynomial Right = new();
            Right.Monomio.Add(new Monomio(1, new Letter("x", 1)));
            Right.Monomio.Add(new Monomio(1));

            equation.Left = new Area(false, Left, Type.Polynomial);
            equation.Right = new Area(false, Right, Type.Polynomial);

            area.MType = Type.Equation;
            area.Space = equation;

            Graphic graphic = new(0, 1, 1, area);
            graphic.Prepare();
            Assert.IsTrue(graphic.ListCoordinates.Count == 2 && graphic.ListCoordinates[0].Y == 1 && graphic.ListCoordinates[1].Y == 2);
        }

        [TestMethod]
        public void Tridimensional()
        {
            Area area = Area.Parse("+E[+P[1*z^1]=+P[1*x^1;1*y^1]]");

            Graphic grapich = new(0, 1, 1, area)
            {
                Tridimensional = true
            };
            grapich.Prepare();
            Assert.IsTrue(grapich.ListCoordinates.Count == 4);
        }
    }
}
