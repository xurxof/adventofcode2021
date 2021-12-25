using AoC9;
using NUnit.Framework;

namespace AoC24.Test
{
    [TestFixture]
    class ProcessorTest
    {
        [Test]
        public void FirstExample()
        {
            var Alu = new Alu(@"inp x
mul x -1");
            var Records = Alu.Process ("3");

            // asert
            Assert.AreEqual(-3, Records.r.X);
        }

        [Test]
        public void SecondExample_ThreeTimesGreater ()
        {
            var Alu = new Alu (@"inp z
inp x
mul z 3
eql z x");
            var Records = Alu.Process (@"26");

            // asert
            Assert.AreEqual (1, Records.r.Z);
        }

        [Test]
        public void SecondExample_NotThreeTimesGreater ()
        {
            var Alu = new Alu (@"inp z
inp x
mul z 3
eql z x");
            var Records = Alu.Process (@"25");

            // asert
            Assert.AreEqual (0, Records.r.Z);
        }


        [Test]
        public void ThirdExample_ToBinary ()
        {
            var Alu = new Alu (@"inp w
add z w
mod z 2
div w 2
add y w
mod y 2
div w 2
add x w
mod x 2
div w 2
mod w 2");
            var Records = Alu.Process (@"5");

            // asert
            Assert.AreEqual (0, Records.r.W);
            Assert.AreEqual (1, Records.r.X);
            Assert.AreEqual (0, Records.r.Y);
            Assert.AreEqual (1, Records.r.Z);
        }

        [Test]
        public void DivideByZero ()
        {
            var Alu = new Alu (@"div w 0");
            var Records = Alu.Process (@"");

            // asert
            Assert.IsTrue (Records.Error); 
        }


        [Test]
        public void ModByZero ()
        {
            var Alu = new Alu (@"inp w
mod w 0");
            var Records = Alu.Process (@"1");

            // asert
            Assert.IsTrue (Records.Error);
        }

         

        [Test]
        public void ModByNegative ()
        {
            var Alu = new Alu (@"inp w
mul w -1
inp x
mod x w");
            var Records = Alu.Process (@"11");

            // asert
            Assert.IsTrue (Records.Error);
        }


        


    }
}