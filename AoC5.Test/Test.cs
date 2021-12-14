using NUnit.Framework;

namespace AoC5.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test_Test ()
        {
            Assert.AreEqual (true, true);
        }

        [Test]
        public void Test_ObliqueCoords ()
        {
            // arrange
            var Line = new Line (new[]
            {
                new Program.Coord (1, 1), new Program.Coord (3, 3)
            });
            // action
            var Coords = Line.Coords ();
            // assert
            CollectionAssert.AreEquivalent (new[]
                {
                    new Program.Coord (1, 1), new Program.Coord (2, 2), new Program.Coord (3, 3)
                },
                Coords);
        }

         


        [Test]
        public void Test_ObliqueCoords_ReverseDef ()
        {
            // arrange
            var Line = new Line (new[]
            {
                new Program.Coord (3, 3), new Program.Coord (1, 1)
            });
            // action
            var Coords = Line.Coords ();
            // assert
            CollectionAssert.AreEquivalent (new[]
                {
                    new Program.Coord (1, 1), new Program.Coord (2, 2), new Program.Coord (3, 3)
                },
                Coords);
        }



        [Test]
        public void Test_ObliqueInverseCoords ()
        {
            // arrange
            var Line = new Line (new[]
            {
                new Program.Coord (9,7), new Program.Coord (7, 9)
            });
            // action
            var Coords = Line.Coords ();
            // assert
            CollectionAssert.AreEquivalent (new[]
                {
                    new Program.Coord (9,7), new Program.Coord (8, 8), new Program.Coord (7, 9)
                },
                Coords);
        }



        [Test]
        public void Test_ObliqueInverseCoords_ReverseDef ()
        {
            // arrange
            var Line = new Line (new[]
            {
                new Program.Coord (7, 9), new Program.Coord (9, 7)
            });
            // action
            var Coords = Line.Coords ();
            // assert
            CollectionAssert.AreEquivalent (new[]
                {
                    new Program.Coord (9, 7), new Program.Coord (8, 8), new Program.Coord (7, 9)
                },
                Coords);
        }
    }
}