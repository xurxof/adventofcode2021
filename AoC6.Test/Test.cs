using NUnit.Framework;

namespace AoC6.Test
{
    //public class TestsFish
    //{
    //    private Fish _f;

    //    [Test]
    //    public void Fish_Internal3_FourDays ()
    //    {
    //        // action
    //        _f.AddDay ();
    //        _f.AddDay ();
    //        _f.AddDay ();

    //        var r = _f.AddDay ();
    //        // assert
    //        Assert.AreEqual (6, _f.InternalTimer);
    //        Assert.AreEqual (8, r.InternalTimer);
    //    }

    //    [Test]
    //    public void Fish_Internal3_OneDay ()
    //    {
    //        // action
    //        _f.AddDay ();
    //        // assert
    //        Assert.AreEqual (2, _f.InternalTimer);
    //    }

    //    [Test]
    //    public void Fish_Internal3_ThreeDays ()
    //    {
    //        // action
    //        _f.AddDay ();
    //        _f.AddDay ();
    //        _f.AddDay ();
    //        // assert
    //        Assert.AreEqual (0, _f.InternalTimer);
    //    }

    //    [Test]
    //    public void Fish_Internal3_TwoDays ()
    //    {
    //        // action
    //        _f.AddDay ();
    //        _f.AddDay ();
    //        // assert
    //        Assert.AreEqual (1, _f.InternalTimer);
    //    }

    //    [SetUp]
    //    public void Setup ()
    //    {
    //        // arrange
    //        _f = new Fish (3);
    //    }
    //}

    public class TestPool
    {
        private Pool _StandardTest;

        [SetUp]
        public void SetUp ()
        {
            _StandardTest = new Pool ("3,4,3,1,2");
        }

        [Test]
        public void Test_GivenTest_1Days ()
        { 
            // action
            var numF = _StandardTest.AddDays (1);
            // assert 
            Assert.AreEqual (5, _StandardTest.Count ());
        }

        [Test]
        public void Test_GivenTest_2Days ()
        {
            // arrange
            // action
            var numF = _StandardTest.AddDays (2);
            // assert 
            Assert.AreEqual (6, _StandardTest.Count ());
        }



        [Test]
        public void Test_GivenTest_3Days ()
        {
            // arrange
            // action
            var numF = _StandardTest.AddDays (3);
            // assert 
            Assert.AreEqual (7, numF);
        }
        [Test]
        public void Test_GivenTest_18Days ()
        { 
            // action
            var numF = _StandardTest.AddDays (18);
            // assert
            Assert.AreEqual (26, numF);
        }


        [Test]
        public void Test_GivenTest_80Days ()
        {
            // arrange
            var _P = new Pool ("3,4,3,1,2");
            // action
            var numF = _P.AddDays (80);
            // assert
            Assert.AreEqual (5934, numF);
        }



        [Test]
        public void Test_GivenTest_256Days ()
        {
            // arrange
            var _P = new Pool ("3,4,3,1,2");
            // action
            var numF = _P.AddDays (256);
            // assert
            Assert.AreEqual (26984457539, numF);
        }
        [Test]
        public void Test_AddDays ()
        {
            var _P = new Pool ("3,4,3");
            //action
            _P.AddDays (4);
            Assert.AreEqual (5, _P.Count());
        }

        [Test]
        public void Test_NewFishes ()
        {
            var _P = new Pool ("3,4,3,0");
            //action
            _P.AddDay ();
            Assert.AreEqual (5, _P.Count ());
        }

        [Test]
        public void Test_OneDay ()
        {
            var _P = new Pool ("3,4,3");
            //action
            _P.AddDay ();
            Assert.AreEqual (3, _P.Count ());
        }

        [Test]
        public void Test_ToString ()
        {
            var _P = new Pool ("3,4,3,1,2");
            Assert.AreEqual (5, _P.Count ());
        }
    }
}