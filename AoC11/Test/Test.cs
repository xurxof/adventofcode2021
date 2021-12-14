using NUnit.Framework;

namespace AoC11.Test
{

    [TestFixture]
    class Test_TestCave
    {
        [SetUp]
        public void SetUp ()
        {
            _Cave = new Cave (Input.Test);

        }

        private Cave _Cave;

        [Test]
        public void Test_OneStep ()
        {
            // action
            _Cave.Steps (1);

            // asert
            Assert.AreEqual (@"6594254334
3856965822
6375667284
7252447257
7468496589
5278635756
3287952832
7993992245
5957959665
6394862637",
                _Cave.ToString ());
        }

        [Test]
        public void Test_TwoStep ()
        {
            // action
            _Cave.Steps (2);

            // asert
            Assert.AreEqual (@"8807476555
5089087054
8597889608
8485769600
8700908800
6600088989
6800005943
0000007456
9000000876
8700006848",
                _Cave.ToString ());
        }

        [Test]
        public void Test_TenStep ()
        {
            // action
            var i = _Cave.Steps (10);

            // asert
            Assert.AreEqual (204, i);
        }

        [Test]
        public void Test_AllBright ()
        {
            // action
            var i = _Cave.StepsUntilAllBright();

            // asert
            Assert.AreEqual (195, i);
        }
    }

    [TestFixture]
    class Test_MiniCave
    {
        [SetUp]
        public void SetUp ()
        {
            _Cave = new Cave (Input.MiniTest);
        }

        private Cave _Cave;

        [Test]
        public void Test_ToString ()
        {
            // asert
            Assert.AreEqual (Input.MiniTest, _Cave.ToString ());
        }

        [Test]
        public void Test_OneStep ()
        {
            // action
            var i =_Cave.Steps (1);
            // asert
            Assert.AreEqual (@"34543
40004
50005
40004
34543", _Cave.ToString ());
            Assert.AreEqual (9,i);
        }


        [Test]
        public void Test_TwoStep ()
        {
            // action
            var i =_Cave.Steps (2);
            // asert
            Assert.AreEqual (9,i);
            Assert.AreEqual (@"45654
51115
61116
51115
45654",
                _Cave.ToString ());
        }
    }
}