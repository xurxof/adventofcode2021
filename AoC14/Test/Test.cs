using System.Linq;
using NUnit.Framework;

namespace AoC14.Test
{
    [TestFixture]
    class Test
    {
        [Test]
        public void Test_OneStep ()
        {
            PolyBuilder B = new PolyBuilder (@"NN

CH -> B
NN -> B");
            B.Steps (1);
            // asert
            Assert.AreEqual (Points ("NBN"), B.Points);
        }

        [Test]
        public void Test_TwoStep_SecondNoProductive ()
        {
            PolyBuilder B = new PolyBuilder (@"NN

CH -> B
NN -> B");
            B.Steps (2);
            // asert
            //Assert.AreEqual ("NBN", B.Points);
            Assert.AreEqual (2 - 1, B.Points);
        }

        [Test]
        public void Test_TwoStep_SecondProductive ()
        {
            PolyBuilder B = new PolyBuilder (@"NN

CH -> B
NN -> B
BN -> C");
            B.Steps (2);
            // asert
            // Assert.AreEqual ("NBCN", B.Poly);
            Assert.AreEqual (1, B.Points);
        }

        [Test]
        public void Test_TestInput_OneStep ()
        {
            PolyBuilder B = new PolyBuilder (Input.Test);
            B.Steps (1);
            // asert
            //Assert.AreEqual ("NCNBCHB", B.Poly);
            Assert.AreEqual (1, B.Points);
        }

        [Test]
        public void Test_TestInput_TwoSteps ()
        {
            PolyBuilder B = new PolyBuilder (Input.Test);
            B.Steps (2);
            // asert
            //Assert.AreEqual ("NBCCNBBBCBHCB", B.Poly);
            Assert.AreEqual (5, B.Points);
        }

        [Test]
        public void Test_TestInput_TreeSteps ()
        {
            PolyBuilder B = new PolyBuilder (Input.Test);
            B.Steps (3);
            // asert
            // Assert.AreEqual ("NBBBCNCCNBBNBNBBCHBHHBCHB", B.Poly);

            Assert.AreEqual (Points ("NBBBCNCCNBBNBNBBCHBHHBCHB"), B.Points);
        }

        private long Points (string input)
        {
            var r = input.GroupBy (c => c).Select (kv => kv.Count ());
            return r.Max () - r.Min ();
        }

        [Test]
        public void Test_TestInput_FourSteps ()
        {
            PolyBuilder B = new PolyBuilder (Input.Test);
            B.Steps (4);
            // asert
            // Assert.AreEqual ("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB", B.Poly);
            Assert.AreEqual (Points ("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB"), B.Points);
        }

        [Test]
        public void Test_TestInput_TenSteps ()
        {
            PolyBuilder B = new PolyBuilder (Input.Test);
            B.Steps (10);
            // asert
            Assert.AreEqual (1588, B.Points);
        }

        [Test]
        public void Test_TestInput_40Steps ()
        {
            PolyBuilder b = new PolyBuilder (Input.Test);
            b.Steps (40);
            // asert
            Assert.AreEqual (2188189693529, b.Points);
        }
    }
}