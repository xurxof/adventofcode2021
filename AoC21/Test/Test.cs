using AoC21;
using NUnit.Framework;

namespace AoC8.test
{
    [TestFixture]
    class BoardTest
    {
        [Test]
        public void Play_OneTurn ()
        {
            // asert
            var B = new Board(4,8);
            B.Play (1);
            // posicion, pntos, tiradas
            Assert.AreEqual ((10,10,1), B.Player1);
            Assert.AreEqual ((8, 0, 0), B.Player2);
        }

        [Test]
        public void Play_TwoTurn ()
        {
            // asert
            var B = new Board (4, 8);
            B.Play (2);
            Assert.AreEqual ((10, 10, 1), B.Player1);
            Assert.AreEqual ((3, 3, 1), B.Player2);
        }

        [Test]
        public void Play  ()
        {
            // asert
            var B = new Board (4, 8);
            var loserScore = B.PlayAndGetLoserScore ();
            Assert.AreEqual (739785, loserScore); 
        }
    }
}