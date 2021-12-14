using NUnit.Framework;

namespace AoC3.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup ()
        {
        }

        [Test]
        public void CalcGammaBinary_TestData ()
        {
            var (gamma, epsilon) = Program.CalcGammaBinary (Input.P1_Test.ToArray ());
            
            // 
            Assert.AreEqual ("10110", gamma);
            Assert.AreEqual ("01001", epsilon);
        }

        [Test]
        public void CalcOxygenCo2_TestData ()
        {
            var (oxygen, co2) = Program.CalcOxygenCo2 (Input.P1_Test.ToArray ());
            // 
            Assert.AreEqual ("10111", oxygen);
            Assert.AreEqual ("01010", co2);
        }
    }
}