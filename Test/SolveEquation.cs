using Matematics.Basic;
using Matematics.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class Easy
    {
        [TestMethod]
        public void OnlyMove()
        {
            Equation equation = new();

            //x + 1 = 0
            Polynomial Left = new();
            Left.Monomio.Add(new Monomio(+1, new Letter[] { new Letter('x', 1) }));
            Left.Monomio.Add(new Monomio(+1));
            Polynomial Right = new();
            Right.Monomio.Add(new Monomio(0));

            //x = -1
            Polynomial Left2 = new();
            Left2.Monomio.Add(new Monomio(1, new Letter[] { new Letter('x', 1) }));
            Polynomial Right2 = new();
            Right2.Monomio.Add(new Monomio(0));
            Right2.Monomio.Add(new Monomio(-1));

            //Equation final = new Equation();
            //final.Left = new Area(false, Left2, Matematics.Other.Type.Polynomial);
            //final.Right = new Area(false, Right2, Matematics.Other.Type.Polynomial);

            equation.Left = new Area(false, Left, Type.Polynomial);
            equation.Right = new Area(false, Right, Type.Polynomial);

            equation.Solve(new Letter("x", 1));
            //Assert.IsTrue(((Polynomial)equation.Left.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Left.Space).Monomio) && ((Polynomial)equation.Right.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Right.Space).Monomio));
        }

        [TestMethod]
        public void MoveAndSum()
        {
            Equation equation = new();

            //x + 1 + 1 = 0
            Polynomial Left = new();
            Left.Monomio.Add(new Monomio(+1, new Letter[] { new Letter('x', 1) }));
            Left.Monomio.Add(new Monomio(+1));
            Left.Monomio.Add(new Monomio(+1));
            Polynomial Right = new();
            Right.Monomio.Add(new Monomio(0));

            //x = -2
            Polynomial Left2 = new();
            Left2.Monomio.Add(new Monomio(1, new Letter[] { new Letter('x', 1) }));
            Polynomial Right2 = new();
            Right2.Monomio.Add(new Monomio(0));
            Right2.Monomio.Add(new Monomio(-1 - 1));

            //Equation final = new Equation();
            //final.Left = new Area(false, Left2, Matematics.Other.Type.Polynomial);
            //final.Right = new Area(false, Right2, Matematics.Other.Type.Polynomial);

            equation.Left = new Area(false, Left, Type.Polynomial);
            equation.Right = new Area(false, Right, Type.Polynomial);

            equation.Solve(new Letter("x", 1));
            //Assert.IsTrue(((Polynomial)equation.Left.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Left.Space).Monomio) && ((Polynomial)equation.Right.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Right.Space).Monomio));
        }

        [TestMethod]
        public void MoltiplyAndDivide()
        {
            Equation equation = new();

            //2x + 1 + 1 = 0
            Polynomial Left = new();
            Left.Monomio.Add(new Monomio(2, new Letter[] { new Letter('x', 1) }));
            Left.Monomio.Add(new Monomio(+1));
            Left.Monomio.Add(new Monomio(+1));
            Polynomial Right = new();
            Right.Monomio.Add(new Monomio(0));

            //x = -1
            Polynomial Left2 = new();
            Left2.Monomio.Add(new Monomio(1, new Letter[] { new Letter('x', 1) }));
            Polynomial Right2 = new();
            Right2.Monomio.Add(new Monomio(0));
            Right2.Monomio.Add(new Monomio(-1));

            //Equation final = new Equation();
            //final.Left = new Area(false, Left2, Matematics.Other.Type.Polynomial);
            //final.Right = new Area(false, Right2, Matematics.Other.Type.Polynomial);

            equation.Left = new Area(false, Left, Type.Polynomial);
            equation.Right = new Area(false, Right, Type.Polynomial);

            equation.Solve(new Letter("x", 1));
            //Assert.IsTrue(((Polynomial)equation.Left.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Left.Space).Monomio) && ((Polynomial)equation.Right.Space).Monomio.AreEqual<Monomio>(((Polynomial)final.Right.Space).Monomio));
        }
    }
}
