using NUnit.Framework;

namespace AoC25.Test
{
    [TestFixture]
    class Day25
    {
        [Test]
        public void OneStep ()
        {
            // asert
            Cucumbers c = new Cucumbers ("...>>>>>...");
            c.Step ();
            Assert.AreEqual ("...>>>>.>..", c.ToString ());
        }

        [Test]
        public void Bidimensiona_OneStep ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"..........
.>v....v..
.......>..
..........");
            c.Step ();
            Assert.AreEqual (@"..........
.>........
..v....v>.
..........", c.ToString ());
        }


        [Test]
        public void FourSteps_Example  ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"...>...
.......
......>
v.....>
......>
.......
..vvv..");
            c.Step ();
            Assert.AreEqual (@"..vv>..
.......
>......
v.....>
>......
.......
....v..",
                c.ToString ());

            // step 2

            c.Step ();
            Assert.AreEqual (@"....v>.
..vv...
.>.....
......>
v>.....
.......
.......",
                c.ToString ());

            // step 3

            c.Step ();
            Assert.AreEqual (@"......>
..v.v..
..>v...
>......
..>....
v......
.......",
                c.ToString ());

            // step 4

            c.Step ();
            Assert.AreEqual (@">......
..v....
..>.v..
.>.v...
...>...
.......
v......",
                c.ToString ());
        }


        [Test]
        public void ComplexExample ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>");
            c.Step ();
            Assert.AreEqual (@"....>.>v.>
v.v>.>v.v.
>v>>..>v..
>>v>v>.>.v
.>v.v...v.
v>>.>vvv..
..v...>>..
vv...>>vv.
>.v.v..v.v",
                c.ToString ());
        }

        [Test]
        public void ComplexExample_58steps ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>");
            for (int i = 0; i < 58; i++)
            {
                c.Step ();    
            }
            
            Assert.AreEqual (@"..>>v>vv..
..v.>>vv..
..>>v>>vv.
..>>>>>vv.
v......>vv
v>v....>>v
vvv.....>>
>vv......>
.>v.vv.v..",
                c.ToString ());
        }

        [Test]
        public void ChangedTest ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"..>>v>vv..
..v.>>vv..
..>>v>>vv.
..>>>>>vv.
v......>vv
v>v....>>v
vvv.....>>
>vv......>
.>v.vv.v..");

            Assert.IsFalse (c.Step ());

        }

        [Test]
        public void StepUntil ()
        {
            // asert
            Cucumbers c = new Cucumbers (@"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>");

            var steps = c.StepUntil ();
            Assert.AreEqual(58,steps);

        }
    }
}