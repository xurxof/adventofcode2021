using AoC9;
using NUnit.Framework;

namespace AoC8.test
{
    [TestFixture]
    class Test_RevisitableCaves
    {
        [Test]
        public void Test_RevisitableCave ()
        {
            var _Cave = new Cave (@"start-A
A-b
b-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (2, _Cave.NumRoutes);
        }


        [Test]
        public void Test_RevisitableSmallCave ()
        {
            var _Cave = new Cave (@"start-A
start-b
A-c
A-b
b-x
A-end
b-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (36, _Cave.NumRoutes);
        }

        [Test]
        public void Test_RevisitableMediumCave ()
        {
            var _Cave = new Cave (@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (103, _Cave.NumRoutes);
        }


        [Test]
        public void Test_RevisitableLargerCave ()
        {
            var _Cave = new Cave (@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW");
            // asert
            _Cave.Walk();
            Assert.AreEqual (3509, _Cave.NumRoutes);
        }

        [Test]
        public void Test_RevisitableProblemCave ()
        {
            var _Cave = new Cave (@"pg-CH
pg-yd
yd-start
fe-hv
bi-CH
CH-yd
end-bi
fe-RY
ng-CH
fe-CH
ng-pg
hv-FL
FL-fe
hv-pg
bi-hv
CH-end
hv-ng
yd-ng
pg-fe
start-ng
end-FL
fe-bi
FL-ks
pg-start");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (36, _Cave.NumRoutes);
        }
    }

    [TestFixture]
    class Test_Caves
    {
        [Test]
        public void Test_LinearCave ()
        {
            var _Cave = new Cave (@"start-A
A-b
b-c
c-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (1, _Cave.NumRoutes);
        }

        [Test]
        public void Test_TwoEndCave ()
        {
            var _Cave = new Cave (@"start-A
A-b
b-c
c-end
b-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (2, _Cave.NumRoutes);
        }

        [Test]
        public void Test_TwoStartCave ()
        {
            var _Cave = new Cave (@"start-z
z-b
start-b
b-c
c-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (2, _Cave.NumRoutes);
        }

        [Test]
        public void Test_DiamondCave ()
        {
            var _Cave = new Cave (@"start-z
z-b
b-c
z-j
j-k
k-c
c-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (2, _Cave.NumRoutes);
        }

        [Test]
        public void Test_NotRepeatSmallCave ()
        {
            var _Cave = new Cave (@"start-z
z-b
b-c
c-a
c-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (1, _Cave.NumRoutes);
        }

        [Test]
        public void Test_SmallTestCave ()
        {
            var _Cave = new Cave (@"start-A
start-b
A-c
A-b
b-x
A-end
b-end");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (10, _Cave.NumRoutes);
        }

        [Test]
        public void Test_MediumTestCave ()
        {
            var _Cave = new Cave (@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (19, _Cave.NumRoutes);
        }

        [Test]
        public void Test_LargerTestCave ()
        {
            var _Cave = new Cave (@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW");
            // asert
            _Cave.Walk ();
            Assert.AreEqual (226, _Cave.NumRoutes);
        }

        [Test]
        public void Test_ProblemTestCave () { }
    }
}