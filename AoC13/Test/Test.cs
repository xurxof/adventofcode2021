using AoC9;
using NUnit.Framework;

namespace AoC13.Test
{
    [TestFixture]
    class Test_Fold
    {

        [Test]
        public void Test_SimpleXFold ()
        {
            var F = new Fold (@"0,0

fold along x=1");
            // asert
            var r = F.Process ();
            System.Diagnostics.Debug.Write (F.ToString ());
            Assert.AreEqual (1, r);
        }

        [Test]
        public void Test_SimpleYFold ()
        {
            var F = new Fold (@"0,0

fold along y=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (1, r);
        }


        [Test]
        public void Test_SimpleY_ChangeBelow_Fold ()
        {
            var F = new Fold (@"0,1

fold along y=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (1, r);
        }


        [Test]
        public void Test_SimpleX_ChangeRight_Fold ()
        {
            var F = new Fold (@"1,0

fold along x=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (1, r);
        }

        [Test]
        public void Test_SimpleY_OverlapBelow_Fold ()
        {
            var F = new Fold (@"0,0
0,1

fold along y=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (1, r);
        }


        [Test]
        public void Test_SimpleX_OverlapRight_Fold ()
        {
            var F = new Fold (@"0,0
1,0

fold along x=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (1, r);
        }
        [Test]
        public void Test_SimpleY_NoOverlapBelow_Fold ()
        {
            var F = new Fold (@"0,0
1,1

fold along y=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (2, r);
        }


        [Test]
        public void Test_SimpleX_NoOverlapRight_Fold ()
        {
            var F = new Fold (@"0,0
1,1

fold along x=1");
            // asert
            var r = F.Process ();
            Assert.AreEqual (2, r);
        }


        [Test]
        public void Test_SimpleExample_Fold ()
        {
            var F = new Fold (@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5");

            System.Diagnostics.Debug.Write (F.ToString ());

            // asert

            var r = F.Process ();
            System.Diagnostics.Debug.Write (F.ToString());
            Assert.AreEqual (17, r);
            r = F.Process ();
            System.Diagnostics.Debug.Write (F.ToString ());
        }

        
    }
}