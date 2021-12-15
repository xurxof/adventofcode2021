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
            Assert.AreEqual(2, C.Risk);
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
            Cave C = new Cave (@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581");
            C.Search ();
            // asert
            Assert.AreEqual (40, C.Risk);
        }

    }
}