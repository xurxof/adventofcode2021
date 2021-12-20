using System.Drawing;
using AoC20;
using AoC9;
using NUnit.Framework;

namespace AoC8.test
{
    [TestFixture]
    class NegativeIndexedArray
    {
        [Test]
        public void Constructor ()
        {
            var a = new NegativeIndexedArray<int> (rows: 3, cols: 4, margin: 3, defaultValue: 0);
            Assert.AreEqual (-3, a.LowerBound (0));
            Assert.AreEqual (5, a.UpperBound (0));
            Assert.AreEqual (-3, a.LowerBound (1));
            Assert.AreEqual (6, a.UpperBound (1));
        }

        [Test]
        public void Set ()
        {
            var a = new NegativeIndexedArray<int> (rows: 3, cols: 4, margin: 3, defaultValue: -1);
            a[0, 0] = 1;
            a[1, 1] = 2;
            a[1, 2] = 3;
            Assert.AreEqual (1, a[0, 0]);
            Assert.AreEqual (2, a[1, 1]);
            Assert.AreEqual (3, a[1, 2]);
            Assert.AreEqual (-1, a[-1, -1]);
        }
    }

    [TestFixture]
    class TestScanner
    {

        [Test]
        public void Neighbors_Middle ()
        {
            Scanner S = new Scanner (Input.Test, 50);

            Assert.AreEqual ("000100010", S.NeighborsString (2, 2));
            // asert
        }
        [Test]
        public void Neighbors_Corner ()
        {
            Scanner S = new Scanner (Input.Test,50);
            Assert.AreEqual ("000010010", S.NeighborsString (0,0));

        }


        [Test]
        public void CalculateEnhalcement ()
        {
            Scanner S = new Scanner(Input.Test,50);

            var value = S.CalculateEnhalcement (2, 2);
            Assert.AreEqual ('1', value);
        }
        [Test]
        public void GetNeightboursCornerValue ()
        {
            Scanner S = new Scanner(Input.Test,75);

            var value = S.CalculateEnhalcement (-2, -2);

            // asert
            Assert.AreEqual ('0', value);
        }

        [Test]
        public void EnhalceImage_Once ()
        {
            Scanner S = new Scanner (Input.Test,100);

            S.EnhalceImage (1);

            // asert
            Assert.AreEqual (24, S.LightPixels);
        }

        [Test]
        public void EnhalceImage_Twice ()
        {
            Scanner S = new Scanner (Input.Test, 100);

            S.EnhalceImage (2);

            // asert
            Assert.AreEqual (35, S.LightPixels);
        }

        [Test]
        public void EnhalceImage_50 ()
        {
            Scanner S = new Scanner (Input.Test, 100);

            S.EnhalceImage (50);

            // asert
            Assert.AreEqual (3351, S.LightPixels);
        }
    }
}