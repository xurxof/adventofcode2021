using NUnit.Framework;

namespace AoC15.Test
{
    [TestFixture]
    class Test
    {
        [Test]
        public void Test_TwoByTwoCave ()
        {
            Cave C = new Cave (@"11
21");
            C.Search ();
            // asert
            Assert.AreEqual (2, C.Risk);
        }


        [Test]
        public void Test_ThreeByThreeCave ()
        {
            Cave C = new Cave (@"111
221
221");
            C.Search ();
            // asert
            Assert.AreEqual (4, C.Risk);
        }


        [Test]
        public void Test_TestCave ()
        {
            Cave C = new Cave (Input.Test);
            C.Search ();
            // asert
            Assert.AreEqual (40, C.Risk);
        }

        [Test]
        public void Test_TestCave_Problem2 ()
        {
            Cave C = new Cave (Input.Test, true);
            C.Search ();
            // asert
            Assert.AreEqual (315, C.Risk);
        }
    }
}