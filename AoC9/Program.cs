using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC9
{
    class Program
    {
        private static List<(int Val, int I, int J)> ExtractLowers (
            List<List<int>> inputInt)
        {
            var Hight = inputInt.Count;
            var Width = inputInt.First ().Count;

            List<(int, int, int)> Sum = new List<(int, int, int)> ();
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (IsLowPoint (i, j, inputInt, Hight, Width))
                    {
                        Sum.Add ((inputInt[i][j], i, j));
                    }
                }
            }
            return Sum;
        }

        private static bool IsLowPoint (int i,
            int j,
            List<List<int>> inputInt,
            int Hight,
            int Width)
        {
            var top = i == 0
                ? 10
                : inputInt[i - 1][j];
            var bottom = i == Hight - 1
                ? 10
                : inputInt[i + 1][j];
            var left = j == 0
                ? 10
                : inputInt[i][j - 1];
            var right = j == Width - 1
                ? 10
                : inputInt[i][j + 1];

            var val = inputInt[i][j];
            return val < top && val < bottom && val < left && val < right;
        }

        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs  & https://www.youtube.com/playlist?list=PL7pkSK1xbGD6g2_BSXgScugMC1CJkkdBW
            Debug.WriteLine (Part1 (Input.Test.ToInput ()));
            Debug.WriteLine (Part2 (Input.Problem.ToInput ()));
        }

        private static int Part1 (List<string> input)
        {
            var inputInt = input.Select (i => i.Select (c => int.Parse (c.ToString ()))
                    .ToList ())
                .ToList ();
            var Sum = ExtractLowers (inputInt);
            return Sum.Sum (v => v.Val + 1);
        }

        private static int Part2 (List<string> input)
        {
            var inputInt = input.Select (i => i.Select (c => int.Parse (c.ToString ()))
                    .ToList ())
                .ToList ();

            var Hight = inputInt.Count;
            var Width = inputInt.First ().Count;
            var Lowers = ExtractLowers (inputInt);
            List<int> _BasinSizes = new List<int> ();
            foreach (var lower in Lowers)
            {
                var _Q = new Queue<(int Val, int I, int J)> ();
                var _Basin = new List<(int Val, int I, int J)> ();

                _Q.Enqueue (lower);
                while (_Q.Any ())
                {
                    var currentLow = _Q.Dequeue ();
                    if (!_Basin.Contains (currentLow))
                    {
                        _Basin.Add (currentLow);
                    }
                    var i = currentLow.I;
                    var j = currentLow.J;
                    // top
                    var Candidate = (Val: i == 0
                        ? 9
                        : inputInt[i - 1][j], I: i - 1, J: j);

                    AddIfNeeded (_Basin, Candidate, currentLow, _Q);
                    // bottom
                    Candidate = (Val: i == Hight - 1
                        ? 9
                        : inputInt[i + 1][j], I: i + 1, J: j);
                    AddIfNeeded (_Basin, Candidate, currentLow, _Q);

                    // left
                    Candidate = (Val: j == 0
                        ? 9
                        : inputInt[i][j - 1], I: i, J: j - 1);
                    AddIfNeeded (_Basin, Candidate, currentLow, _Q);

                    // right
                    Candidate = (Val: j == Width - 1
                        ? 9
                        : inputInt[i][j + 1], I: i, J: j + 1);
                    AddIfNeeded (_Basin, Candidate, currentLow, _Q);

                }
                _BasinSizes.Add(_Basin.Count);
            }
            return _BasinSizes.OrderByDescending (i => i).Take (3).Aggregate ((a, b) => a * b);
        }

        private static void AddIfNeeded (List<(int Val, int I, int J)> _Basin,
            (int Val, int I, int J) Candidate,
            (int Val, int I, int J) currentLow,
            Queue<(int Val, int I, int J)> _Q)
        {
            if (!_Basin.Contains (Candidate) &&
                Candidate.Val > currentLow.Val &&
                Candidate.Val < 9)
            {
                _Q.Enqueue (Candidate);
            }
        }
    }
}