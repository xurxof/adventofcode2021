using NUnit.Framework;

namespace AoC8.test
{
    [TestFixture]
    class TestSegments
    {
        [Test]
        public void Test_Digest ()
        {
            // action
            var result = Program.Digest (Input.Test);
            // asert
            Assert.AreEqual (26, result);
        }

        [Test]
        public void Test_Digest2 ()
        {
            // action
            var result = Program.Digest2 ("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");
            // asert
            Assert.AreEqual ("5353", result[0]);
        }

        [Test]
        public void Test_Digest2_b ()
        {
            // action
            var result = Program.Digest2 ("be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe");
            // asert
            Assert.AreEqual (26, result);
        }
    }
}