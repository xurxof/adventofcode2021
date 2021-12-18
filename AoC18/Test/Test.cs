using System.Linq;
using NUnit.Framework;

namespace AoC18.Test
{
    [TestFixture]
    class Test
    {
        [Test]
        public void ConstructorFromString ()
        {
            var N = Number.FromString ("[1,2]");
            Assert.AreEqual (1, N.Left.Value);
            Assert.AreEqual (2, N.Right.Value);
            Assert.IsNull (N.Parent);
        }


        [Test]
        public void ConstructorFromString_TwoDigits ()
        {
            var N = Number.FromString ("[15,25]");
            Assert.AreEqual (15, N.Left.Value);
            Assert.AreEqual (25, N.Right.Value);
            Assert.IsNull (N.Parent);
        }

        [Test]
        public void ConstructorFromString_TwoLevels ()
        {
            var N = Number.FromString ("[[1,2],[3,4]]");
            Assert.AreEqual (1, N.Left.Left.Value);
            Assert.AreEqual (2, N.Left.Right.Value);
            Assert.AreEqual (3, N.Right.Left.Value);
            Assert.AreEqual (4, N.Right.Right.Value);
        }

        [Test]
        public void ConstructorFromString_RecursiveRight ()
        {
            var N = Number.FromString ("[7,[6,[5,[4,[3,2]]]]]");
            Assert.AreEqual (2, N.Right.Right.Right.Right.Right.Value);
        }

        [Test]
        public void EnumerateLeftRight ()
        {
            var N = Number.FromString ("[7,[6,[5,[4,[3,2]]]]]");
            CollectionAssert.AreEqual (new[] { 7, 6, 5, 4, 3, 2 },
                N.EnumerateValuesLeftRight ()
                    .Select (c => c.Value));
        }

        [Test]
        public void EnumerateRightLeft ()
        {
            var N = Number.FromString ("[7,[6,[5,[4,[3,2]]]]]");
            CollectionAssert.AreEqual (new[] { 2, 3, 4, 5, 6, 7 },
                N.EnumerateValuesRightLeft ()
                    .Select (c => c.Value));
        }

        [Test]
        public void Sum ()
        {
            var N1 = Number.FromString ("[[1,2],[3,4]]");

            var N2 = Number.FromString ("[5,6]");
            var R = N1.Add (N2);
            Assert.AreEqual ("[[[1,2],[3,4]],[5,6]]", R.ToString ());
            Assert.AreEqual (5, R.Right.Left.Value);
        }

        [Test]
        public void ExplodeToRight ()
        {
            var N = Number.FromString ("[[[[[9,8],1],2],3],4]");
            var ToExplode = N.Left.Left.Left.Left;
            ToExplode.Explode ();

            Assert.AreEqual ("[[[[0,9],2],3],4]", N.ToString ());
        }

        [Test]
        public void ExplodeToLeft ()
        {
            var N = Number.FromString ("[7,[6,[5,[4,[3,2]]]]]");
            var ToExplode = N.Right.Right.Right.Right;
            ToExplode.Explode ();

            Assert.AreEqual ("[7,[6,[5,[7,0]]]]", N.ToString ());
        }

        [Test]
        public void Explode_RecursiveRightNumberRight ()
        {
            var N = Number.FromString ("[[6,[5,[4,[3,2]]]],1]");
            var ToExplode = N.Left.Right.Right.Right;
            ToExplode.Explode ();

            Assert.AreEqual ("[[6,[5,[7,0]]],3]", N.ToString ());
        }


        [Test]
        public void Explode_Example4()
        {
            var N = Number.FromString ("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            var ToExplode = N.Find(7,3);
            ToExplode.Explode ();

            Assert.AreEqual ("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", N.ToString ());
        }


        [Test]
        public void Explode_Example5 ()
        {
            var N = Number.FromString ("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            var ToExplode = N.Find (3, 2);
            ToExplode.Explode ();

            Assert.AreEqual ("[[3,[2,[8,0]]],[9,[5,[7,0]]]]", N.ToString ());
        }


        [Test]
        public void Explode_Example6 ()
        {
            var N = Number.FromString ("[[[[12,12],[6,14]],[[15,0],[17,[8,1]]]],[2,9]]");
            var ToExplode = N.Find (8, 1);
            ToExplode.Explode ();

            Assert.AreEqual ("[[[[12,12],[6,14]],[[15,0],[25,0]]],[3,9]]", N.ToString ());
        }


        [Test]
        public void Split_Example1 ()
        {
            var N = Number.FromString ("[[[[0,7],4],[15,[0,13]]],[1,1]]");
            var ToSplit= N.Find (15);
            ToSplit.SplitLeft ();

            Assert.AreEqual ("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", N.ToString ());
        }

        [Test]
        public void Split_Example2 ()
        {
            var N = Number.FromString ("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]");
            var ToSplit = N.Find (13);
            ToSplit.SplitRight ();

            Assert.AreEqual ("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", N.ToString ());
        }

        [Test]
        public void Reduce()
        { 
            var N = Number.FromString ("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
            N.Reduce();
            

            Assert.AreEqual ("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", N.ToString ());
        }


        [Test]
        public void Sum_SmallSum1 ()
        {
            var N = Number.Sum(@"[1,1]
[2,2]
[3,3]
[4,4]");
            

            Assert.AreEqual ("[[[[1,1],[2,2]],[3,3]],[4,4]]", N.ToString ());
        }


        [Test]
        public void Sum_SmallSum2 ()
        {
            var N = Number.Sum (@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]");


            Assert.AreEqual ("[[[[3,0],[5,3]],[4,4]],[5,5]]", N.ToString ());
        }



        [Test]
        public void Sum_SmallSum3 ()
        {
            var N = Number.Sum (@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]
[6,6]");


            Assert.AreEqual ("[[[[5,0],[7,4]],[5,5]],[6,6]]", N.ToString ());
        }


        [Test]
        public void Sum_SmallSum4 ()
        {
            var N = Number.Sum (@"[[[[4,3],4],4],[7,[[8,4],9]]]
[1,1]");


            Assert.AreEqual ("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", N.ToString ());
        }

        [Test]
        public void Sum_FullSlightlySum ()
        {
            var N = Number.Sum (@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]");


            Assert.AreEqual ("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", N.ToString ());
        }


        [Test]
        public void Magnitude_RegularNumber ()
        {
            var N = Number.Sum (@"[9,1]");


            Assert.AreEqual (29, N.Magnitude);
        }


        [Test]
        public void Magnitude_Recursive ()
        {
            var N = Number.Sum (@"[[9,1],[1,9]]");


            Assert.AreEqual (129, N.Magnitude);
        }


        [Test]
        public void Magnitude_Recursive2 ()
        {
            var N = Number.Sum (@"[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");


            Assert.AreEqual (3488, N.Magnitude);
        }


        [Test]
        public void Sum_slightlySum1 ()
        {
            var N = Number.Sum (@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");


            Assert.AreEqual ("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]", N.ToString ());
        }

        [Test]
        public void Sum_slightlySum2 ()
        {
            var N = Number.Sum (@"[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]");


            Assert.AreEqual ("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]", N.ToString ());
        }


        [Test]
        public void Sum_slightlySum3 ()
        {
            var N = Number.Sum (@"[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]");


            Assert.AreEqual ("[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]", N.ToString ());
        }

        [Test]
        public void Sum_slightlySum6 ()
        {
            var N = Number.Sum (@"[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]
[2,9]");


            Assert.AreEqual ("[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]", N.ToString ());
        }


        [Test]
        public void Sum_FullExample ()
        {
            var N = Number.Sum (@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]");
            

            Assert.AreEqual ("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", N.ToString ());

            Assert.AreEqual (4140, N.Magnitude);
        }

        [Test]
        public void Sum_Combinations ()
        {
            var N = Number.Combination (@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]");


            // Assert.AreEqual ("[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]", N.ToString ());

            Assert.AreEqual (3993, N);
        }


    }
}