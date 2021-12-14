using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace AoC4.Test
{
    public class BoardTests
    {
        private Board _Board;

        [Test]
        public void Board_Creation ()
        {
            Assert.AreEqual (0, _Board.Marks);
            Assert.IsFalse (_Board.HasLine);
        }

        [Test]
        public void Board_MarkInexistente ()
        {
            _Board.Mark (99999);
            Assert.AreEqual (0, _Board.Marks);
            Assert.IsFalse (_Board.HasLine);
        }

        [Test]
        public void Board_MarkLine ()
        {
            _Board.Mark (22);
            _Board.Mark (13);
            _Board.Mark (17);
            _Board.Mark (11);
            _Board.Mark (0);

            Assert.AreEqual (5, _Board.Marks);
            Assert.IsTrue (_Board.HasLine);
        }

        [Test]
        public void Board_MarkOne ()
        {
            _Board.Mark (22);
            Assert.AreEqual (1, _Board.Marks);
            Assert.IsFalse (_Board.HasLine);
        }

        [Test]
        public void Board_VerticalOne ()
        {
            _Board.Mark (22);
            _Board.Mark (8);
            _Board.Mark (21);
            _Board.Mark (6);
            _Board.Mark (1); 

            Assert.AreEqual (5, _Board.Marks);
            Assert.IsTrue (_Board.HasLine);
        }

        [SetUp]
        public void Setup ()
        {
            _Board = new Board (@"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19");
        }
    }

    [TestFixture]
    public class BoardsTest
    {
        [Test]
        public void Board_StandardTest ()
        {
            var input = Input.GetValues (Input.Test);
            Boards b = new Boards (input.Boards);
            var result = b.Drew (input.DrawedNumers);
            Assert.AreEqual (188, result.win.First().UnmarkedSum);
            Assert.AreEqual (24, result.num);
        }



        [Test]
        public void Board_LastSimpleTest ()
        {
            var input = Input.GetValues (Input.Test);
            Boards b = new Boards (input.Boards);
            var result = b.Last (input.DrawedNumers);
            Assert.AreEqual (148, result.win.First().UnmarkedSum);
            Assert.AreEqual (13, result.num);
        }



        [Test]
        public void Board_LastProblemTest ()
        {
            var input = Input.GetValues (Input.Problem);
            Boards b = new Boards (input.Boards);
            var result = b.Last (input.DrawedNumers);
            Debug.WriteLine (result.win.First ().UnmarkedSum);
            Debug.WriteLine (result.num);
            }



        [Test]
        public void Board_ProblemTest ()
        {
            var input = Input.GetValues (Input.Problem);
            Boards b = new Boards (input.Boards);
            var result = b.Drew (input.DrawedNumers);
            // 41128 failed (53*776)
            // (17 * 829)
            
        }
    }
}