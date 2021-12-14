using NUnit.Framework;

namespace AoC7.test
{
    public class Tests
    {
        [SetUp]
        public void Setup ()
        {
        }

        [Test]
        public void TestSeudosum ()
        {
            Assert.AreEqual (3, Program.Seudosum (1, 3));
        }

        [Test]
        public void TestSeudosum2 ()
        {
            Assert.AreEqual (3, Program.Seudosum (2, 4));
        }

        [Test]
        public void TestSeudosum2_inv ()
        {
            Assert.AreEqual (3, Program.Seudosum (4, 2));
        }

        [Test]
        public void TestSeudosum3 ()
        {
            Assert.AreEqual (6, Program.Seudosum (2, 5));
        }

        [Test]
        public void TestSeudosum3_Inv ()
        {
            Assert.AreEqual (6, Program.Seudosum (5, 2));
        }
    }
}